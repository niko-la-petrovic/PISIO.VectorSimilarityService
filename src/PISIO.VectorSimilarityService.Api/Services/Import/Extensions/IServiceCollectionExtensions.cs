
using PISIO.VectorSimilarityService.Api.Services.Import.Manager;

namespace PISIO.VectorSimilarityService.Api.Services.Import.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddImportServices(this IServiceCollection services)
    {
        services.AddScoped<IImportManager, ImportManager>();
        return services;
    }
}
