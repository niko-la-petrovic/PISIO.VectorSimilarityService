using Microsoft.EntityFrameworkCore;
using PISIO.VectorSimilarityService.Api.Controllers;
using PISIO.VectorSimilarityService.Api.Dtos;
using PISIO.VectorSimilarityService.Api.Exceptions;
using PISIO.VectorSimilarityService.Data;
using PISIO.VectorSimilarityService.Dtos.Collection;

namespace PISIO.VectorSimilarityService.Api.Services.Collection.Repository.EntityFramework;

public class EFCollectionRepository : ICollectionRepository
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger _logger;

    public EFCollectionRepository(
        IApplicationDbContext dbContext,
        ILogger<EFCollectionRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<CreateCollectionResponse> AddAsync(
        CreateCollectionRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating collection {Name}", request.Name);

        var model = new Models.Collection
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Vectors = new List<Models.Vector>(),
            EmbeddingSize = request.EmbeddingSize
        };

        _dbContext.Collections.Add(model);
        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Collection {Name} created", request.Name);

        var response = new CreateCollectionResponse(
            model.Id,
            model.Name,
            model.Description,
            model.CreatedAt,
            model.EmbeddingSize);

        return response;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting collection with id {Id}", id);
        await _dbContext.Collections
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
        _logger.LogInformation("Collection with id {Id} deleted", id);
    }

    public async Task<Paginated<GetCollectionResponse>> GetAllAsync(GetCollectionsRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting collections");
        var query = _dbContext.Collections.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.NameQuery))
            query = query.Where(c => c.Name.Contains(request.NameQuery));

        var total = await query.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling((double)total / request.PageSize);

        var responseItems = await query
            .OrderBy(c => c.Name)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(c => new GetCollectionResponse(
                c.Id,
                c.Name,
                c.Description,
                c.CreatedAt,
                c.LastUpdated,
                c.EmbeddingSize))
            .ToListAsync(cancellationToken);

        var response = new Paginated<GetCollectionResponse>
        {
            Items = responseItems,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = total,
            TotalPages = totalPages
        };
        return response;
    }

    public async Task<GetCollectionResponse> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting collection with id {Id}", id);

        var model = await _dbContext.Collections
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken: cancellationToken) ?? throw EntityNotFoundException.Create<Models.Collection, Guid>(id);
        _logger.LogInformation("Collection with id {Id} found", id);

        var response = new GetCollectionResponse(
            model.Id,
            model.Name,
            model.Description,
            model.CreatedAt,
            model.LastUpdated,
            model.EmbeddingSize);

        return response;
    }

    public async Task UpdateAsync(
        UpdateCollectionRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating collection with id {Id}", request.Id);

        var updatedRows = await _dbContext.Collections
            .Where(c => c.Id == request.Id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(c => c.Name, c => request.Name)
                .SetProperty(c => c.Description, c => request.Description),
                cancellationToken);

        _logger.LogInformation("Update of collection with id {Id} affected {Updated} rows", request.Id, updatedRows);
    }
}
