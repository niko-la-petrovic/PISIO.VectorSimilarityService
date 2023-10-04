namespace PISIO.VectorSimilarityService.Api.Controllers;

public record UpdateVectorRequest(
    Guid Id,
    string Class,
    string? Description,
    Guid CollectionId,
    float[] Embedding);