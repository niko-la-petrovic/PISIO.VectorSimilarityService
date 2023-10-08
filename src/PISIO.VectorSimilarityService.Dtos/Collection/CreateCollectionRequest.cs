using System.ComponentModel.DataAnnotations;

namespace PISIO.VectorSimilarityService.Dtos.Collection;

public record CreateCollectionRequest(
    [MinLength(1)] string Name,
    string? Description,
    int? EmbeddingSize);
