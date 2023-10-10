using Microsoft.Extensions.Logging.Abstractions;
using PISIO.VectorSimilarityService.Api.Services.Vector.Repository;
using PISIO.VectorSimilarityService.Dtos.VectorSimilarity;

namespace PISIO.VectorSimilarityService.Api.Services.VectorSimilarity;

public class VectorSimilarityService : IVectorSimilarityService
{
    private ILogger _logger;
    private readonly IVectorRepository _vectorRepository;

    public VectorSimilarityService(
        ILogger<VectorSimilarityService> logger,
        IVectorRepository vectorRepository)
    {
        _logger = logger;
        _vectorRepository = vectorRepository;
    }

    public Task<VectorSimilarityResponse> FindClassesAsync(
        VectorSimilarityRequest request,
        CancellationToken cancellationToken)
    {
        var algorithm = request.Algorithm;
        switch (algorithm)
        {
            case VectorSimilarityConstants.Algorithm.Flat:
                return FindClassesFlatAsync(request, cancellationToken);
            default:
                throw new NotImplementedException();
        }
    }

    private async Task<VectorSimilarityResponse> FindClassesFlatAsync(
        VectorSimilarityRequest request,
        CancellationToken cancellationToken)
    {
        var embedding = request.Embedding;
        var kNearest = new List<(float, string)>();

        var allVectors = await _vectorRepository.GetAllRawAsync(
            request.CollectionId,
            cancellationToken);

        if (allVectors.FirstOrDefault(x => x.Embedding.Length != embedding.Length) is not null)
            throw new ArgumentException("Embedding length mismatch");

        foreach (var vector in allVectors)
        {
            var d = CalculateDistance(embedding, vector.Embedding, request.Distance);
            if (kNearest.Count < request.K || kNearest.Count == 0)
            {
                kNearest.Add((d, vector.Class));
            }
            else
            {
                var max = kNearest.Max(x => x.Item1);
                if (d < max)
                {
                    kNearest.Remove(kNearest.First(x => x.Item1 == max));
                    kNearest.Add((d, vector.Class));
                }
            }
        }

        var ordered = kNearest.OrderBy(kNearest => kNearest.Item1).ToList();
        var classes = ordered.Select(x => x.Item2).ToArray();
        var distances = ordered.Select(x => x.Item1).ToArray();

        return new VectorSimilarityResponse(classes, distances);
    }

    public float CalculateDistance(
        float[] embedding1,
        float[] embedding2,
        string distance)
    {
        return distance switch
        {
            VectorSimilarityConstants.Distance.L2 => CalculateEuclideanDistance(embedding1, embedding2),
            VectorSimilarityConstants.Distance.Cosine => CalculateCosineDistance(embedding1, embedding2),
            VectorSimilarityConstants.Distance.IP => CalculateInnerProduct(embedding1, embedding2),
            _ => throw new NotImplementedException(),
        };
    }

    // TODO use SIMD

    private float CalculateInnerProduct(float[] embedding1, float[] embedding2)
    {
        var d = 0f;
        for (int i = 0; i < embedding1.Length; i++)
        {
            d += embedding1[i] * embedding2[i];
        }
        return d;
    }

    private float CalculateCosineDistance(float[] embedding1, float[] embedding2)
    {
        var d = 0f;
        for (int i = 0; i < embedding1.Length; i++)
        {
            d += embedding1[i] * embedding2[i];
        }
        return d / (CalculateEuclideanDistance(embedding1, embedding1) * CalculateEuclideanDistance(embedding1, embedding2));
    }

    private float CalculateEuclideanDistance(float[] embedding1, float[] embedding2)
    {
        var d = 0f;
        for (int i = 0; i < embedding1.Length; i++)
        {
            d += (embedding1[i] - embedding2[i]) * (embedding1[i] - embedding2[i]);
        }
        return (float)Math.Sqrt(d);
    }
}
