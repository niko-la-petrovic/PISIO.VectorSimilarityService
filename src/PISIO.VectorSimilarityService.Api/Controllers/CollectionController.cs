﻿using PISIO.VectorSimilarityService.Api.Services.Collection.Manager;
using PISIO.VectorSimilarityService.Dtos.Collection;

namespace PISIO.VectorSimilarityService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CollectionController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly ICollectionManager _collectionManager;

    public CollectionController(
        ILogger<CollectionController> logger,
        ICollectionManager collectionManager
        )
    {
        _logger = logger;
        _collectionManager = collectionManager;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetCollectionResponse>> GetCollection(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting collection with id {Id}", id);
        var response = await _collectionManager.GetCollectionAsync(id, cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<CreateCollectionResponse>> PostCreateCollection(
        [FromBody] CreateCollectionRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating collection with name {Name}", request.Name);
        var response = await _collectionManager.CreateCollectionAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetCollection), new { id = response.Id }, response);
    }
}