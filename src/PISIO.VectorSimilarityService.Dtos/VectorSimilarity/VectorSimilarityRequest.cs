namespace PISIO.VectorSimilarityService.Dtos.VectorSimilarity;

public record VectorSimilarityRequest(
    Guid CollectionId,
    float[] Embedding,
    string Algorithm,
    int K,
    string Distance);