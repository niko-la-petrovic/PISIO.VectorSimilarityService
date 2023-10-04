using PISIO.VectorSimilarityService.Api.Controllers;
using PISIO.VectorSimilarityService.Api.Dtos;
using PISIO.VectorSimilarityService.Dtos.Vector;

namespace PISIO.VectorSimilarityService.Api.Services.Vector.Manager;

public interface IVectorManager
{
    Task<CreateVectorResponse> CreateVectorAsync(CreateVectorRequest request, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<Paginated<GetVectorResponse>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken);
    Task<GetVectorResponse> GetVectorAsync(Guid id, CancellationToken cancellationToken);
    Task<Paginated<GetVectorResponse>> GetVectorsAsync(Guid collectionId, int page, int pageSize, CancellationToken cancellationToken);
    Task UpdateAsync(Guid id, UpdateVectorRequest request, CancellationToken cancellationToken);
}