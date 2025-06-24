using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure;
public class CatalogContext : DbContext
{
    public CatalogContext(DbContextOptions<CatalogContext> options) : base(options) { }

    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        SeedBooks(modelBuilder);
    }

    private void SeedBooks(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().HasData(
            new Book { Id = 1, Title = "7 Habits Of Highly Effective People", Author = "Stephen Covey", PublicationDate = new DateOnly(1989, 08, 15) },
            new Book { Id = 2, Title = "Richest Man In Babylon", Author = "George Samuel Clason", PublicationDate = new DateOnly(1926, 01, 01) }
        );
    }
}
