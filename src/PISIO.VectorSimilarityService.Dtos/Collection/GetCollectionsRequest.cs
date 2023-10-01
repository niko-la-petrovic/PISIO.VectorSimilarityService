namespace PISIO.VectorSimilarityService.Dtos.Collection;

public record GetCollectionsRequest(string? NameQuery, int Page = 1, int PageSize =
5);
