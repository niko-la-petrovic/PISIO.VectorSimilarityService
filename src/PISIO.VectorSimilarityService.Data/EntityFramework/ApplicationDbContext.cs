using Microsoft.EntityFrameworkCore;
using PISIO.VectorSimilarityService.Models;

namespace PISIO.VectorSimilarityService.Data.EntityFramework;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<Collection> Collections { get; protected set; }
    public DbSet<Vector> Vectors { get; protected set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Collection>(c =>
        {
            c.HasKey(c => c.Id);

            c.HasIndex(c => c.Name);
        });

        modelBuilder.Entity<Vector>(v =>
        {
            v.HasKey(v => v.Id);

            v.HasOne(v => v.Collection)
                .WithMany(c => c.Vectors)
                .HasForeignKey(v => v.CollectionId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is ICreated || e.Entity is IUpdated);

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added && entry.Entity is ICreated created)
                created.CreatedAt = DateTime.UtcNow;

            if (entry.State == EntityState.Modified && entry.Entity is IUpdated updated)
                updated.LastUpdated = DateTime.UtcNow;
        }
        return base.SaveChangesAsync(cancellationToken);
    }

    Task IApplicationDbContext.SaveChangesAsync(CancellationToken cancellationToken)
    {
        return SaveChangesAsync(cancellationToken);
    }
}
