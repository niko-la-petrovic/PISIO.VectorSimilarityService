using Microsoft.AspNetCore.Http;

namespace PISIO.VectorSimilarityService.Dtos.Import;

public class ImportVectorsRequest
{
    public required Guid CollectionId { get; set; }
    public required bool HasHeader { get; set; }
    public required IFormFile File { get; set; }
}
