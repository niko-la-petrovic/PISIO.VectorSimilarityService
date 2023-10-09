using PISIO.VectorSimilarityService.Dtos.Import;

namespace PISIO.VectorSimilarityService.Api.Services.Import.Manager;

public interface IImportManager
{
    Task ImportAsync(ImportVectorsRequest request, CancellationToken cancellationToken);
}