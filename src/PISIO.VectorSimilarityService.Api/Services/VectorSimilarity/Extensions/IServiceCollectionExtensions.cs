namespace PISIO.VectorSimilarityService.Api.Services.VectorSimilarity.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddVectorSimilarityServices(this IServiceCollection services)
    {
        // TODO maybe change to singleton for caching purposes
        services.AddScoped<IVectorSimilarityService, VectorSimilarityService>();
        return services;
    }
}
