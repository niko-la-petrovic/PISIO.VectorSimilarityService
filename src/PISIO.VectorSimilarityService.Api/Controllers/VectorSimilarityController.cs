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

    [HttpPost]
    public ActionResult<string> PostFromFile(

        )
    {
        return Ok("Hello from VectorSimilarityController");
    }
}
