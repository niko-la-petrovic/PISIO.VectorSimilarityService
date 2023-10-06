using Microsoft.AspNetCore.HttpOverrides;
using PISIO.VectorSimilarityService.Api.Filters.Exception.Extensions;
using PISIO.VectorSimilarityService.Api.Services.Extensions;

var builder = WebApplication.CreateBuilder(args);

var builderServices = builder.Services;
var builderConfiguration = builder.Configuration;
var builderEnvironment = builder.Environment;
var isDevelopment = builderEnvironment.IsDevelopment();

builderConfiguration.AddJsonFile($"appsettings.{builderEnvironment.EnvironmentName}.Local.json", optional: true, reloadOnChange: true);

builderServices.Configure<ForwardedHeadersOptions>(options =>
{
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();

    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedProto |
        ForwardedHeaders.XForwardedFor;
});
builderServices.AddCors(options =>
{
    if (isDevelopment)
        options.AddDefaultPolicy(builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});
builderServices.AddApiServices(builderConfiguration);
builderServices.AddControllers(options =>
{
    options.AddExceptionFilters();
});
builderServices.AddEndpointsApiExplorer();
builderServices.AddSwaggerGen();

var app = builder.Build();

app.UseCors();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
