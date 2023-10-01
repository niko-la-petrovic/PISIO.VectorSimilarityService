using PISIO.VectorSimilarityService.Api.Services.Extensions;

var builder = WebApplication.CreateBuilder(args);

var builderServices = builder.Services;
var builderConfiguration = builder.Configuration;
var builderEnvironment = builder.Environment;

builderConfiguration.AddJsonFile($"appsettings.{builderEnvironment.EnvironmentName}.Local.json", optional: true, reloadOnChange: true);

builderServices.AddApiServices(builderConfiguration);
builderServices.AddControllers();
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
