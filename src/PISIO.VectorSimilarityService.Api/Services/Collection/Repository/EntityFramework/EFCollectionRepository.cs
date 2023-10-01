using Microsoft.EntityFrameworkCore;
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
            Name = request.Name,
            Description = request.Description,
            Vectors = new List<Models.Vector>()
        };

        _dbContext.Collections.Add(model);
        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Collection {Name} created", request.Name);

        var response = new CreateCollectionResponse(
            model.Id,
            model.Name,
            model.Description,
            model.CreatedAt);

        return response;
    }

    public async Task<GetCollectionResponse> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting collection with id {Id}", id);

        var model = await _dbContext.Collections
            .FirstOrDefaultAsync(c => c.Id == id) ?? throw EntityNotFoundException.Create<Models.Collection, Guid>(id);
        _logger.LogInformation("Collection with id {Id} found", id);

        var response = new GetCollectionResponse(
            model.Id, model.Name, model.Description, model.CreatedAt, model.LastUpdated);

        return response;
    }
}
