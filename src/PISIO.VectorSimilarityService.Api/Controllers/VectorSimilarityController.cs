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
        _logger.LogInformation("Received request vector similarity request with {Count} embeddings", request.Embeddings.Length);
        if (request.Embeddings.Length == 0)
        {
            _logger.LogError
                ("Received request vector similarity request with no embeddings");
            return BadRequest("At least two embeddings are required");
        }

        _logger.LogInformation("Finding classes for embeddings {Count}", request.Embeddings.Length);
        var res = await _vectorSimilarityService.FindClassesAsync(
            request.CollectionId,
            request.Embeddings,
            cancellationToken);
        _logger.LogInformation("Found {Count} classes", res.Length);

        return Ok(res);
    }

    [HttpPost("from-file")]
    public ActionResult<string> PostFromFile()
    {
        // TODO parse file
        // TODO call service on embeddings
        // TODO implement action
        return Ok("Hello from VectorSimilarityController");
    }
}
