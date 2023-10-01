using PISIO.VectorSimilarityService.Api.Dtos;
using PISIO.VectorSimilarityService.Dtos.Vector;

namespace PISIO.VectorSimilarityService.Api.Services.Vector.Repository;

public interface IVectorRepository
{
    Task<CreateVectorResponse> AddAsync(CreateVectorRequest request, CancellationToken cancellationToken);
    Task<GetVectorResponse> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<Paginated<GetVectorResponse>> GetFromCollectionAsync(Guid collectionId, int page, int pageSize, CancellationToken cancellationToken);
}