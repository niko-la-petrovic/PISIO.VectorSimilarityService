using Microsoft.EntityFrameworkCore;
using PISIO.VectorSimilarityService.Models;

namespace PISIO.VectorSimilarityService.Data.EntityFramework;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<Collection> Collections { get; protected set; }
    public DbSet<Vector> Vectors { get; protected set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasPostgresExtension("vector");

        modelBuilder.Entity<Collection>(c =>
        {
            c.HasKey(c => c.Id);

            c.HasIndex(c => c.Name);
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}
