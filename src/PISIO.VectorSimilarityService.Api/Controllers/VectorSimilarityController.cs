namespace PISIO.VectorSimilarityService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VectorSimilarityController : ControllerBase
{
    private ILogger<VectorSimilarityController> _logger;

    public VectorSimilarityController(ILogger<VectorSimilarityController> logger)
    {
        _logger = logger;
    }

    [HttpPost("from-file")]
    public ActionResult<string> PostFromFile(

        )
    {
        // TODO implement action
        return Ok("Hello from VectorSimilarityController");
    }
}
