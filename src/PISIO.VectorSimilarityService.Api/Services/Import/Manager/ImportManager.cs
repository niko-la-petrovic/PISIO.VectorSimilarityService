using PISIO.VectorSimilarityService.Api.Services.Vector.Manager;
using PISIO.VectorSimilarityService.Dtos.Import;
using PISIO.VectorSimilarityService.Dtos.Vector;
using System.Diagnostics;

namespace PISIO.VectorSimilarityService.Api.Services.Import.Manager;

public class ImportManager : IImportManager
{
    private readonly ILogger _logger;
    private readonly IVectorManager _vectorManager;

    public ImportManager(
        ILogger<ImportManager> logger,
        IVectorManager vectorManager)
    {
        _logger = logger;
        _vectorManager = vectorManager;
    }

    public async Task ImportAsync(
        ImportVectorsRequest request,
        CancellationToken cancellationToken)
    {
        var sw = new Stopwatch();
        sw.Start();
        try
        {
            _logger.LogInformation("Importing vectors from file {FileName}", request.File.FileName);

            using var stream = request.File.OpenReadStream();
            using var reader = new StreamReader(stream);
            var hasHeader = request.HasHeader;
            var requests = new List<CreateVectorRequest>();

            var classIndex = 0;
            var descriptionIndex = 1;
            var embeddingIndex = 2;
            if (hasHeader)
            {
                var header = await reader.ReadLineAsync(cancellationToken);
                if (header is null)
                {
                    _logger.LogError("Header is null");
                    throw new ArgumentException("Header is null", nameof(request));
                }

                // TODO replace hardcoded params with request params
                var headerValues = header.Split(',');
                classIndex = Array.IndexOf(headerValues, "class");
                descriptionIndex = Array.IndexOf(headerValues, "description");
                embeddingIndex = Array.IndexOf(headerValues, "embedding");
                _logger.LogInformation("Header values {HeaderValues}", headerValues);
            }

            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync(cancellationToken);
                if (line is null)
                {
                    _logger.LogError("Line is null");
                    throw new ArgumentException("Line is null", nameof(request));
                }

                var values = line.Split(',');
                if (values.Length != 3)
                {
                    _logger.LogError("Line {Line} does not have 3 values", line);
                    throw new ArgumentException("Line does not have 3 values", nameof(request));
                }

                // TODO replace harcoded params with request params
                var classValue = values[classIndex];
                var description = values[descriptionIndex];
                var embedding = values[embeddingIndex]
                    .Split('|')
                    .Select(float.Parse)
                    .ToArray();
                requests.Add(new CreateVectorRequest(classValue, description, request.CollectionId, embedding));
            }
            // TODO ensure that all vectors have the same length
            // TODO pass collectionId to ensure that all vectors are in the same collection

            await _vectorManager.CreateVectorsAsync(requests, cancellationToken);
        }
        finally
        {
            sw.Stop();
            _logger.LogInformation("Imported vectors from file {FileName} in {ElapsedMilliseconds} ms", request.File.FileName, sw.ElapsedMilliseconds);
        }
    }
}
