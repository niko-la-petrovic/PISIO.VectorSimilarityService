namespace PISIO.VectorSimilarityService.Dtos.Vector;

public record CreateVectorResponse(
    Guid Id,
    string Class,
    string? Description,
    Guid CollectionId,
    float[] Embedding);
