namespace PISIO.VectorSimilarityService.Api.Services.VectorSimilarity;

public interface IVectorSimilarityService
{
    Task<string[]> FindClassesAsync(
        Guid collectionId,
        float[] embeddings,
        CancellationToken cancellationToken);
}