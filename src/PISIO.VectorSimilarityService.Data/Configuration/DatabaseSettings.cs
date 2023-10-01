namespace PISIO.VectorSimilarityService.Data.Configuration;

public class DatabaseSettings
{
    public required string Provider { get; init; }
    public required string ConnectionString { get; init; }
}

