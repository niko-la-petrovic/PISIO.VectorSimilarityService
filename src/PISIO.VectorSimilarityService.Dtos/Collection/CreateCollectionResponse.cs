namespace PISIO.VectorSimilarityService.Dtos.Collection;

public record CreateCollectionResponse(Guid Id, string Name, string Description, DateTime CratedAt);
