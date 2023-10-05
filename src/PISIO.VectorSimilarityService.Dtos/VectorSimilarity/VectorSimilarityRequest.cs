namespace PISIO.VectorSimilarityService.Dtos.VectorSimilarity;

public record VectorSimilarityRequest(
    Guid CollectionId,
    float[] Embeddings);