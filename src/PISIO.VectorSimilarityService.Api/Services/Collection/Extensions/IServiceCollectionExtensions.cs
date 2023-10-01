using PISIO.VectorSimilarityService.Api.Services.Collection.Manager;
using PISIO.VectorSimilarityService.Api.Services.Collection.Repository;
using PISIO.VectorSimilarityService.Api.Services.Collection.Repository.EntityFramework;

namespace PISIO.VectorSimilarityService.Api.Services.Collection.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddCollectionServices(this IServiceCollection services)
    {
        services.AddScoped<ICollectionManager, CollectionManager>();
        services.AddScoped<ICollectionRepository, EFCollectionRepository>();
        return services;
    }
}
