using PISIO.VectorSimilarityService.Api.Dtos;
using PISIO.VectorSimilarityService.Dtos.Collection;

namespace PISIO.VectorSimilarityService.Api.Services.Collection.Manager;

public interface ICollectionManager
{
    Task<CreateCollectionResponse> CreateCollectionAsync(CreateCollectionRequest request, CancellationToken cancellationToken);
    Task<GetCollectionResponse> GetCollectionAsync(Guid id, CancellationToken cancellationToken);
    Task<Paginated<GetCollectionResponse>> GetCollectionsAsync(GetCollectionsRequest request, CancellationToken cancellationToken);
}