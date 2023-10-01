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
        ApplicationDbContext dbContext
        )
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<CreateVectorResponse> AddAsync(CreateVectorRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating vector with class {Class}", request.Class);
        var model = new Models.Vector
        {
            Class = request.Class,
            CollectionId = request.CollectionId,
            Embedding = request.Embedding,
            Description = request.Description
        };

        _dbContext.Vectors.Add(model);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Created vector with id {Id}", model.Id);

        return new CreateVectorResponse(model.Id, model.Class, model.Description, model.CollectionId, model.Embedding);
    }

    public async Task<GetVectorResponse> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting vector with id {Id}", id);
        var model = await _dbContext.Vectors.FirstOrDefaultAsync(x => x.Id == id,cancellationToken) ??
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
}
