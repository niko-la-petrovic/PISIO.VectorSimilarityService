namespace PISIO.VectorSimilarityService.Dtos.VectorSimilarity;

public record VectorSimilarityResponse(
       string[] Classes,
       float[] Distances);