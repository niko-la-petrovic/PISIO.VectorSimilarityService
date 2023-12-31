﻿using PISIO.VectorSimilarityService.Api.Services.Collection.Extensions;
using PISIO.VectorSimilarityService.Api.Services.Import.Extensions;
using PISIO.VectorSimilarityService.Api.Services.Vector.Extensions;
using PISIO.VectorSimilarityService.Api.Services.VectorSimilarity.Extensions;
using PISIO.VectorSimilarityService.Data.Extensions;

namespace PISIO.VectorSimilarityService.Api.Services.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddApiServices(
        this IServiceCollection services,
        ConfigurationManager builderConfiguration)
    {
        services.AddCollectionServices();
        services.AddVectorServices();
        services.AddVectorSimilarityServices();
        services.AddImportServices();
        services.AddDatabaseServices(builderConfiguration);
        return services;
    }
}
