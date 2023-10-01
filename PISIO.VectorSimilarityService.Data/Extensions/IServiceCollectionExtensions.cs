using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PISIO.VectorSimilarityService.Data.Configuration;
using PISIO.VectorSimilarityService.Data.EntityFramework.Extensions;

namespace PISIO.VectorSimilarityService.Data.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddDatabaseServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var persistenceSettings = configuration.GetRequiredSection(PersistenceConstants.PersistenceSection)
            .Get<PersistenceSettings>() ?? throw new InvalidOperationException("Persistence settings are not configured");

        switch (persistenceSettings.Database.Provider)
        {
            case PersistenceConstants.PostgresVectorProvider:
                services.AddEntityFrameworkServices(persistenceSettings.Database);
                break;
            default:
                // TODO fix exception handling
                throw new InvalidOperationException("Database provider is not supported");
        }
        return services;
    }
}
