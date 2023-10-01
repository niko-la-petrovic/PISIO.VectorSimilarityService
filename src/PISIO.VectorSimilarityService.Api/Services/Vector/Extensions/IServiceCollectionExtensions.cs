
using PISIO.VectorSimilarityService.Api.Services.Vector.Manager;
using PISIO.VectorSimilarityService.Api.Services.Vector.Repository;
using PISIO.VectorSimilarityService.Api.Services.Vector.Repository.EntityFramework;

namespace PISIO.VectorSimilarityService.Api.Services.Vector.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddVectorServices(this IServiceCollection services)
    {
        services.AddScoped<IVectorManager, VectorManager>();
        services.AddScoped<IVectorRepository, EFVectorRepository>();
        return services;
    }
}
