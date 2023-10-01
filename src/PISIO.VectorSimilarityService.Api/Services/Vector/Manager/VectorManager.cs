using PISIO.VectorSimilarityService.Api.Dtos;
using PISIO.VectorSimilarityService.Api.Services.Vector.Repository;
using PISIO.VectorSimilarityService.Dtos.Vector;

namespace PISIO.VectorSimilarityService.Api.Services.Vector.Manager;

public class VectorManager : IVectorManager
{
    private readonly ILogger _logger;
    private readonly IVectorRepository _vectorRepository;

    public VectorManager(
        ILogger<VectorManager> logger,
        IVectorRepository vectorRepository
        )
    {
        _logger = logger;
        _vectorRepository = vectorRepository;
    }

    // TODO add exception filters for EntityNotFoundException

    public async Task<CreateVectorResponse> CreateVectorAsync(CreateVectorRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating vector with class {Class}", request.Class);
        var response = await _vectorRepository.AddAsync(request, cancellationToken);
        _logger.LogInformation("Created vector with id {Id}", response.Id);
        return response;
    }

    public async Task<GetVectorResponse> GetVectorAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting vector with id {Id}", id);
        var response = await _vectorRepository.GetAsync(id, cancellationToken);
        return response;
    }

    public async Task<Paginated<GetVectorResponse>> GetVectorsAsync(Guid collectionId, int page, int pageSize, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting vectors for collection with id {CollectionId}", collectionId);
        var response = await _vectorRepository.GetFromCollectionAsync(collectionId, page, pageSize, cancellationToken);
        return response;
    }
}