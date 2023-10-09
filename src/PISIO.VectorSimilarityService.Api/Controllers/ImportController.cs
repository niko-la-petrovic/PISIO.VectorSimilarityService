using PISIO.VectorSimilarityService.Api.Services.Import.Manager;
using PISIO.VectorSimilarityService.Dtos.Import;

namespace PISIO.VectorSimilarityService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImportController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IImportManager _importManager;
    public ImportController(
        ILogger<ImportController> logger,
        IImportManager importManager)
    {
        _logger = logger;
        _importManager = importManager;
    }

    [HttpPost("vector")]
    public async Task<ActionResult> PostImportVectors(
        [FromForm] ImportVectorsRequest request,
        CancellationToken cancellationToken)
    {
        if (request.File is null)
        {
            _logger.LogWarning("Import request does not contain file");
            return BadRequest();
        }

        _logger.LogInformation("Importing vectors from file {FileName}", request.File.FileName);
        await _importManager.ImportAsync(request, cancellationToken);
        _logger.LogInformation("Imported vectors from file {FileName}", request.File.FileName);
        return Ok();
    }
}
