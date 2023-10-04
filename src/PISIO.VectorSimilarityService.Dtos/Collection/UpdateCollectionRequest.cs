namespace PISIO.VectorSimilarityService.Api.Controllers;

public class UpdateCollectionRequest
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
}