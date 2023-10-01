namespace PISIO.VectorSimilarityService.Models;

public class Collection
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }

    // TODO add relation
}