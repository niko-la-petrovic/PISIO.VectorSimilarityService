# PISIO.VectorSimilarityService

## Scripts

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
