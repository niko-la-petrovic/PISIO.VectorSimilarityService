using Microsoft.EntityFrameworkCore;
using PISIO.VectorSimilarityService.Api.Dtos;
using PISIO.VectorSimilarityService.Api.Exceptions;
using PISIO.VectorSimilarityService.Data.EntityFramework;
using PISIO.VectorSimilarityService.Dtos.Vector;

namespace PISIO.VectorSimilarityService.Api.Services.Vector.Repository.EntityFramework;

public class EFVectorRepository : IVectorRepository
{
    private readonly ILogger _logger;
    private readonly ApplicationDbContext _dbContext;

    public EFVectorRepository(
        ILogger<EFVectorRepository> logger,
        ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<CreateVectorResponse> AddAsync(CreateVectorRequest request, CancellationToken cancellationToken)
    {
        if (request.Embedding.Length == 0)
        {
            _logger.LogError("Embedding cannot be empty");
            throw new ArgumentException("Embedding cannot be empty", nameof(request));
        }

        var collection = await _dbContext.Collections
            .FirstOrDefaultAsync(c => c.Id == request.CollectionId, cancellationToken);
        if (collection is null)
        {
            _logger.LogError("Collection with id {CollectionId} does not exist", request.CollectionId);
            throw EntityNotFoundException.Create<Models.Collection, Guid>(request.CollectionId);
        }

        if (collection.EmbeddingSize is null)
            collection.EmbeddingSize = request.Embedding.Length;
        else if (collection.EmbeddingSize != request.Embedding.Length)
        {
            _logger.LogError("Embedding size {EmbeddingSize} does not match collection embedding size {CollectionEmbeddingSize}", request.Embedding.Length, collection.EmbeddingSize);
            throw new ArgumentException("Embedding size does not match collection embedding size", nameof(request));
        }

        _logger.LogInformation("Creating vector with class {Class}", request.Class);
        var model = new Models.Vector
        {
            Class = request.Class,
            CollectionId = request.CollectionId,
            Embedding = request.Embedding,
            Description = request.Description
        };
        collection.VectorCount++;

        _dbContext.Vectors.Add(model);
        await _dbContext.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Created vector with id {Id}", model.Id);

        return new CreateVectorResponse(model.Id, model.Class, model.Description, model.CollectionId, model.Embedding);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting vector with id {Id}", id);
        using var tx = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var vector = await _dbContext.Vectors
                .FirstOrDefaultAsync(v => v.Id == id, cancellationToken) ??
                throw EntityNotFoundException.Create<Models.Vector, Guid>(id);

            var deleted = await _dbContext.Vectors
                 .Where(v => v.Id == id)
                 .ExecuteDeleteAsync(cancellationToken);

            await _dbContext.Collections
                .Where(c => c.Id == vector.CollectionId)
                .ExecuteUpdateAsync(c =>
                    c.SetProperty(
                        c => c.VectorCount,
                        c => c.VectorCount - 1)
                    .SetProperty(
                        c => c.EmbeddingSize,
                        c => c.VectorCount > 0 ? c.EmbeddingSize : 0),
                    cancellationToken);

            await tx.CommitAsync(cancellationToken);
        }
        catch (Exception)
        {
            tx.Rollback();
        }
        _logger.LogInformation("Deleted vector with id {Id}", id);
    }

    public async Task<Paginated<GetVectorResponse>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all vectors");

        var query = _dbContext.Vectors.AsQueryable();

        var totalCount = await query.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        var responseItems = await query
            .OrderByDescending(
                x => x.LastUpdated != null ? x.LastUpdated : x.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new GetVectorResponse(
                x.Id,
                x.Class,
                x.Description,
                x.CollectionId,
                x.Embedding,
                x.CreatedAt,
                x.LastUpdated))
            .ToListAsync(cancellationToken);
        _logger.LogInformation("Found {Count} vectors", responseItems.Count);

        var response = new Paginated<GetVectorResponse>
        {
            Items = responseItems,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = totalPages
        };

        return response;
    }

    public async Task<GetVectorResponse> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting vector with id {Id}", id);
        var model = await _dbContext.Vectors.FirstOrDefaultAsync(x => x.Id == id, cancellationToken) ??
            throw EntityNotFoundException.Create<Models.Vector, Guid>(id);

        return new GetVectorResponse(model.Id, model.Class, model.Description, model.CollectionId, model.Embedding, model.CreatedAt, model.LastUpdated);
    }

    public async Task<Paginated<GetVectorResponse>> GetFromCollectionAsync(Guid collectionId, int page, int pageSize, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting vectors for collection with id {CollectionId}", collectionId);

        var query = _dbContext.Vectors.Where(x => x.CollectionId == collectionId);

        var totalCount = await query.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        var responseItems = await query
            .OrderByDescending(
                x => x.LastUpdated != null ? x.LastUpdated : x.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new GetVectorResponse(
                x.Id,
                x.Class,
                x.Description,
                x.CollectionId,
                x.Embedding,
                x.CreatedAt,
                x.LastUpdated))
            .ToListAsync(cancellationToken);

        _logger.LogInformation("Found {Count} vectors for collection with id {CollectionId}", responseItems.Count, collectionId);
        var response = new Paginated<GetVectorResponse>(
            page,
            pageSize,
            totalCount,
            totalPages,
            responseItems);

        return response;
    }

    public async Task UpdateAsync(Guid id, UpdateVectorRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating vector with id {Id}", id);
        var model = await _dbContext.Vectors
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken) ??
            throw EntityNotFoundException.Create<Models.Vector, Guid>(id);

        if (request.Embedding.Length == 0)
        {
            _logger.LogError("Embedding cannot be empty");
            throw new ArgumentException("Embedding cannot be empty", nameof(request));
        }

        var collection = await _dbContext.Collections
            .FirstOrDefaultAsync(c => c.Id == model.CollectionId, cancellationToken);
        if (collection is null)
        {
            _logger.LogError("Collection with id {CollectionId} does not exist", model.CollectionId);
            throw EntityNotFoundException.Create<Models.Collection, Guid>(model.CollectionId);
        }

        if (collection.EmbeddingSize is null)
            collection.EmbeddingSize = request.Embedding.Length;
        else if (collection.EmbeddingSize != request.Embedding.Length)
        {
            _logger.LogError("Embedding size {EmbeddingSize} does not match collection embedding size {CollectionEmbeddingSize}", request.Embedding.Length, collection.EmbeddingSize);
            throw new ArgumentException("Embedding size does not match collection embedding size", nameof(request));
        }

        model.Embedding = request.Embedding;
        model.Class = request.Class;
        model.Description = request.Description;
        model.CollectionId = request.CollectionId;

        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated vector with id {Id}", id);
    }
}
