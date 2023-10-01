using PISIO.VectorSimilarityService.Api.Dtos;
using PISIO.VectorSimilarityService.Dtos.Collection;

namespace PISIO.VectorSimilarityService.Api.Services.Collection.Repository;

public interface ICollectionRepository
{
    Task<CreateCollectionResponse> AddAsync(CreateCollectionRequest request, CancellationToken cancellationToken);
    Task<Paginated<GetCollectionResponse>> GetAllAsync(GetCollectionsRequest request, CancellationToken cancellationToken);
    Task<GetCollectionResponse> GetAsync(Guid id, CancellationToken cancellationToken);
}