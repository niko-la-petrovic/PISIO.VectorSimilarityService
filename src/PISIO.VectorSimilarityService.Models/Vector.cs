namespace PISIO.VectorSimilarityService.Models;

public class Vector : ICreated, IUpdated
{
    public Guid Id { get; set; }
    public required string Class { get; set; }
    public string? Description { get; set; }
    public Guid CollectionId { get; set; }
    public required float[] Embedding { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastUpdated { get; set; }

    public required virtual Collection Collection { get; set; }
}
