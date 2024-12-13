using BookStore.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Context;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        //ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public DbSet<Livro> Livro { get; set; }
    public DbSet<Assunto> Assunto { get; set; }
    public DbSet<Autor> Autor { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
    }
}
