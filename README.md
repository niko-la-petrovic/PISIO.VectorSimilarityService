# PISIO.VectorSimilarityService

## Scripts

Docker build

```bash
docker build -t pisio-vec-sim-service-api -f ./src/PISIO.VectorSimilarityService.Api/Dockerfile .
```

### Migrations

#### Tooling

Install dotnet ef tooling

```bash
dotnet tool install --global dotnet-ef
```

Create migration

```bash
dotnet ef migrations add Initial -s ./src/PISIO.VectorSimilarityService.Api/ -p ./src/PISIO.VectorSimilarityService.Data/ -o Migrations
```

Remove migration

```bash
dotnet ef migrations remove -s ./src/PISIO.VectorSimilarityService.Api/ -p ./src/PISIO.VectorSimilarityService.Data/
```

Update database

```bash
dotnet ef database update -s ./src/PISIO.VectorSimilarityService.Api/ -p ./src/PISIO.VectorSimilarityService.Data/
```
