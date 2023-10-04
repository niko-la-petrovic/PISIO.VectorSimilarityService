using PISIO.VectorSimilarityService.Api.Controllers;
using PISIO.VectorSimilarityService.Api.Dtos;
using PISIO.VectorSimilarityService.Api.Exceptions;
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
    
    public async Task DeleteCollectionAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting collection with id {Id}", id);
        var collection = await _collectionRepository.GetAsync(id, cancellationToken);
        if (collection == null)
        {
            _logger.LogInformation("Collection with id {Id} not found", id);
            throw new EntityNotFoundException<Models.Collection, Guid>();
        }

        await _collectionRepository.DeleteAsync(id, cancellationToken);
        _logger.LogInformation("Collection with id {Id} deleted", id);
    }

    public Task<GetCollectionResponse> GetCollectionAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting collection with id {Id}", id);
        return _collectionRepository.GetAsync(id, cancellationToken);
    }

    public async Task<Paginated<GetCollectionResponse>> GetCollectionsAsync(
        GetCollectionsRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting collections");
        var response = await _collectionRepository.GetAllAsync(request, cancellationToken);
        return response;
    }

    public async Task UpdateCollectionAsync(UpdateCollectionRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating collection with id {Id}", request.Id);
        await _collectionRepository.UpdateAsync(request, cancellationToken);
        _logger.LogInformation("Collection with id {Id} updated", request.Id);
    }
}
