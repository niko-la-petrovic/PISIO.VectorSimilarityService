namespace PISIO.VectorSimilarityService.Api.Services.VectorSimilarity;

public class VectorSimilarityService : IVectorSimilarityService
{
    private ILogger _logger;

    public VectorSimilarityService(ILogger<VectorSimilarityService> logger)
    {
        _logger = logger;
    }

    public Task<string[]> FindClassesAsync(
        Guid collectionId,
        float[] embeddings,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Finding classes for embeddings {Count}", embeddings.Length);
        var res = new string[embeddings.Length];

        // TODO implement KNN algorithm
        // TODO calculate KNN cache

        _logger.LogInformation("Found {Count} classes", res.Length);
        return Task.FromResult(res);
    }
}
