namespace PISIO.VectorSimilarityService.Models;

public class Collection : ICreated, IUpdated
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastUpdated { get; set; }
    public int? EmbeddingSize { get; set; }

    public required virtual IEnumerable<Vector> Vectors { get; set; }
}