﻿using PISIO.VectorSimilarityService.Api.Dtos;
using PISIO.VectorSimilarityService.Api.Services.Vector.Repository;
using PISIO.VectorSimilarityService.Dtos.Vector;

namespace PISIO.VectorSimilarityService.Api.Services.Vector.Manager;

public class VectorManager : IVectorManager
{
    private readonly ILogger _logger;
    private readonly IVectorRepository _vectorRepository;

    public VectorManager(
        ILogger<VectorManager> logger,
        IVectorRepository vectorRepository
        )
    {
        _logger = logger;
        _vectorRepository = vectorRepository;
    }

    public async Task<CreateVectorResponse> CreateVectorAsync(CreateVectorRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating vector with class {Class}", request.Class);
        var response = await _vectorRepository.AddAsync(request, cancellationToken);
        _logger.LogInformation("Created vector with id {Id}", response.Id);
        return response;
    }

    public async Task<IEnumerable<CreateVectorResponse>> CreateVectorsAsync(
        IEnumerable<CreateVectorRequest> requests,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating vectors");
        var response = await _vectorRepository.AddAsync(requests, cancellationToken);
        _logger.LogInformation("Created vectors");
        return response;
    }

    public async Task<GetVectorResponse> GetVectorAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting vector with id {Id}", id);
        var response = await _vectorRepository.GetAsync(id, cancellationToken);
        return response;
    }

    public async Task<Paginated<GetVectorResponse>> GetVectorsAsync(Guid collectionId, int page, int pageSize, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting vectors for collection with id {CollectionId}", collectionId);
        var response = await _vectorRepository.GetFromCollectionAsync(collectionId, page, pageSize, cancellationToken);
        return response;
    }

    public async Task<Paginated<GetVectorResponse>> GetAllAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all vectors");
        var response = await _vectorRepository.GetAllAsync(page, pageSize, cancellationToken);
        return response;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting vector with id {Id}", id);
        await _vectorRepository.DeleteAsync(id, cancellationToken);
        _logger.LogInformation("Deleted vector with id {Id}", id);
    }

    public async Task UpdateAsync(Guid id, UpdateVectorRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating vector with id {Id}", id);
        await _vectorRepository.UpdateAsync(id, request, cancellationToken);
        _logger.LogInformation("Updated vector with id {Id}", id);
    }
}