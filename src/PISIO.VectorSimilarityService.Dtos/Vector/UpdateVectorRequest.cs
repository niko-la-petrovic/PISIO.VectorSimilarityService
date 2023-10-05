namespace PISIO.VectorSimilarityService.Dtos.Vector;

public record UpdateVectorRequest(
    Guid Id,
    string Class,
    string? Description,
    Guid CollectionId,
    float[] Embedding);