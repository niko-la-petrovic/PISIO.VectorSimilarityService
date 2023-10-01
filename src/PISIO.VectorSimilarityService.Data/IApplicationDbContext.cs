using Microsoft.EntityFrameworkCore;
using PISIO.VectorSimilarityService.Models;

namespace PISIO.VectorSimilarityService.Data;

public interface IApplicationDbContext
{
    DbSet<Collection> Collections { get; }
    DbSet<Vector> Vectors { get; }

    Task SaveChangesAsync(CancellationToken cancellationToken);
}
