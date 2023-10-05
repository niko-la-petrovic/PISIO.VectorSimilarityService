namespace PISIO.VectorSimilarityService.Dtos.Vector;

public record CreateVectorRequest(
    string Class,
    string? Description,
    Guid CollectionId,
    float[] Embedding);
