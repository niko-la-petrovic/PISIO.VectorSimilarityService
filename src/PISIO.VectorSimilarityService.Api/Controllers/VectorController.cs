using PISIO.VectorSimilarityService.Api.Dtos;
using PISIO.VectorSimilarityService.Api.Services.Vector.Manager;
using PISIO.VectorSimilarityService.Dtos.Vector;

namespace PISIO.VectorSimilarityService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VectorController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IVectorManager _vectorManager;

    public VectorController(
        ILogger<VectorController> logger,
        IVectorManager vectorManager
        )
    {
        _logger = logger;
        _vectorManager = vectorManager;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetVectorResponse>> GetVector(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting vector with id {Id}", id);
        var response = await _vectorManager.GetVectorAsync(id, cancellationToken);
        return Ok(response);
    }

    [HttpGet("collection/{collectionId}")]
    public async Task<ActionResult<Paginated<GetVectorResponse>>> GetVectors(
        CancellationToken cancellationToken,
        [FromRoute] Guid collectionId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        _logger.LogInformation("Getting vectors for collection with id {CollectionId}", collectionId);
        var response = await _vectorManager.GetVectorsAsync(collectionId, page, pageSize, cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<CreateVectorResponse>> PostCreateVector(
        [FromBody] CreateVectorRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating vector with class {Name}", request.Class);
        var response = await _vectorManager.CreateVectorAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetVector), new { id = response.Id }, response);
    }

    [HttpGet]
    public async Task<Paginated<GetVectorResponse>> GetAllVectors(
        CancellationToken cancellationToken,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        _logger.LogInformation("Getting all vectors");
        var response = await _vectorManager.GetAllAsync(page, pageSize, cancellationToken);
        return response;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteVector(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting vector with id {Id}", id);
        await _vectorManager.DeleteAsync(id, cancellationToken);
        _logger.LogInformation("Vector with id {Id} deleted", id);
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> PutVector(
        [FromRoute] Guid id,
        [FromBody] UpdateVectorRequest request,
        CancellationToken cancellationToken)
    {
        if (id != request.Id)
            return BadRequest();

        _logger.LogInformation("Updating vector with id {Id}", id);
        await _vectorManager.UpdateAsync(id, request, cancellationToken);
        _logger.LogInformation("Vector with id {Id} updated", id);
        return NoContent();
    }
}
