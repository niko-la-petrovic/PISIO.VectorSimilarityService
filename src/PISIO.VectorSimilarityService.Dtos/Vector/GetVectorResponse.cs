namespace PISIO.VectorSimilarityService.Dtos.Vector;

public record GetVectorResponse(
    Guid Id,
    string Class,
    string? Description,
    Guid CollectionId,
    float[] Embedding,
    DateTime CreatedAt,
    DateTime? LastUpdated);