using PISIO.VectorSimilarityService.Api.Services.VectorSimilarity;
using PISIO.VectorSimilarityService.Dtos.VectorSimilarity;

namespace PISIO.VectorSimilarityService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VectorSimilarityController : ControllerBase
{
    private ILogger _logger;
    private IVectorSimilarityService _vectorSimilarityService;

    public VectorSimilarityController(
        ILogger<VectorSimilarityController> logger,
        IVectorSimilarityService similarityService)
    {
        _logger = logger;
        _vectorSimilarityService = similarityService;
    }

    [HttpPost]
    public async Task<ActionResult<VectorSimilarityResponse>> GetSimilarity(
        [FromBody] VectorSimilarityRequest request,
        CancellationToken cancellationToken)
    {
        if (!VectorSimilarityConstants.Distance.Distances.Contains(request.Distance))
        {
            _logger.LogError("Received request vector similarity request with an invalid distance {Distance}", request.Distance);
            return BadRequest("Invalid distance provided");
        }

        if (!VectorSimilarityConstants.Algorithm.Algorithms.Contains(request.Algorithm))
        {
            _logger.LogError("Received request vector similarity request with an invalid algorithm {Algorithm}", request.Algorithm);
            return BadRequest("Invalid algorithm provided");
        }

        _logger.LogInformation("Received request vector similarity request for {Algorithm} and k {K}", request.Algorithm, request.K);
        if (request.Embedding.Length == 0)
        {
            _logger.LogError
                ("Received request vector similarity request with an empty embedding");
            return BadRequest("Empty embedding provided");
        }

        var res = await _vectorSimilarityService.FindClassesAsync(
            request,
            cancellationToken);
        _logger.LogInformation("Found {Count} classes", res.Classes.Length);

        return Ok(res);
    }

    [HttpPost("from-file")]
    public ActionResult<string> PostFromFile()
    {
        throw new NotImplementedException();
        // TODO parse file
        // TODO call service on embeddings
        // TODO implement action
        return Ok("Hello from VectorSimilarityController");
    }
}
