using PISIO.VectorSimilarityService.Api.Services.Collection.Manager;
using PISIO.VectorSimilarityService.Dtos.Collection;

namespace PISIO.VectorSimilarityService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ILogger<CategoryController> _logger;
    private readonly ICollectionManager _collectionManager;

    public CategoryController(
        ILogger<CategoryController> logger,
        ICollectionManager collectionManager
        )
    {
        _logger = logger;
        _collectionManager = collectionManager;
    }

    [HttpPost]
    public IActionResult Post([FromBody] PostCollectionRequestDto request)
    {

        return Ok();
    }
}
