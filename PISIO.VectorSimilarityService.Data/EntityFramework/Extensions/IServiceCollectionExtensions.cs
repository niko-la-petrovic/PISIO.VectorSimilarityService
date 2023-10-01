using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pgvector.EntityFrameworkCore;
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
            options.UseNpgsql(databaseSettings.ConnectionString, o => o.UseVector());
        });
        return services;
    }
}
