using PISIO.VectorSimilarityService.Api.Services.Collection.Repository;
using PISIO.VectorSimilarityService.Dtos.Collection;

namespace PISIO.VectorSimilarityService.Api.Services.Collection.Manager;

public class CollectionManager : ICollectionManager
{
    private readonly ILogger _logger;
    private readonly ICollectionRepository _collectionRepository;

    public CollectionManager(
        ILogger<CollectionManager> logger,
        ICollectionRepository collectionRepository)
    {
        _logger = logger;
        _collectionRepository = collectionRepository;
    }

    public async Task<CreateCollectionResponse> CreateCollectionAsync(
        CreateCollectionRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating collection {Name}", request.Name);
        var response = await _collectionRepository.AddAsync(request, cancellationToken);
        _logger.LogInformation("Collection {Name} created", request.Name);
        return response;
    }

    public Task<GetCollectionResponse> GetCollectionAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting collection with id {Id}", id);
        return _collectionRepository.GetAsync(id, cancellationToken);
    }
}
