using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PISIO.VectorSimilarityService.Data.Configuration;

namespace PISIO.VectorSimilarityService.Data.EntityFramework.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddEntityFrameworkServices(
        this IServiceCollection services,
        DatabaseSettings databaseSettings)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(databaseSettings.ConnectionString);
        });
        services.AddScoped<IApplicationDbContext>(
            provider => provider.GetRequiredService<ApplicationDbContext>());
        return services;
    }
}
