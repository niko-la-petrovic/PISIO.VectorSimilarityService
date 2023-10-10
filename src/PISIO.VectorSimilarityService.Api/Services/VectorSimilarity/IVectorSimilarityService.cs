using PISIO.VectorSimilarityService.Dtos.VectorSimilarity;

namespace PISIO.VectorSimilarityService.Api.Services.VectorSimilarity;

public interface IVectorSimilarityService
{
    Task<VectorSimilarityResponse> FindClassesAsync(
        VectorSimilarityRequest request,
        CancellationToken cancellationToken);
}