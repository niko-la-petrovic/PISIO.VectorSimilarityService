namespace PISIO.VectorSimilarityService.Dtos.Collection;

public record GetCollectionResponse(
    Guid Id,
    string Name,
    string Description,
    DateTime CreatedAt,
    DateTime? LastUpdated);
