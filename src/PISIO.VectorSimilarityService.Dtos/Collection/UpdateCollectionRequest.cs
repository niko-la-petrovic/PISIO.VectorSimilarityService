namespace PISIO.VectorSimilarityService.Api.Controllers;

public record UpdateCollectionRequest(
    Guid Id,
    string Name,
    string Description);