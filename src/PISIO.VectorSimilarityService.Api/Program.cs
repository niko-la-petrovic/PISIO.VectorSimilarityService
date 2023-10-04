using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Filters;
using PISIO.VectorSimilarityService.Api.Filters.Exception.Extensions;
using PISIO.VectorSimilarityService.Api.Services.Extensions;
using System.Collections.Immutable;

var builder = WebApplication.CreateBuilder(args);

var builderServices = builder.Services;
var builderConfiguration = builder.Configuration;
var builderEnvironment = builder.Environment;

builderConfiguration.AddJsonFile($"appsettings.{builderEnvironment.EnvironmentName}.Local.json", optional: true, reloadOnChange: true);

builderServices.Configure<ForwardedHeadersOptions>(options =>
{
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();

    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedProto |
        ForwardedHeaders.XForwardedFor;
});
builderServices.AddApiServices(builderConfiguration);
builderServices.AddControllers(options =>
{
    options.AddExceptionFilters();
});
builderServices.AddEndpointsApiExplorer();
builderServices.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
