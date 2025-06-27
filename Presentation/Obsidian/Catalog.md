- [ ] Setup [[Swagger]]
- [ ] Install in Infrastructure :
	- [ ] Microsoft.EntityFrameworkCore.SqlServer
	- [ ] Microsoft.EntityFrameworkCore.Design
	- [ ] Microsoft.EntityFrameworkCore.Tools in Infrastructure
- [ ] Install in Catalog.API :
	- [ ] Microsoft.EntityFrameworkCore.Design
- [ ] Create CatalogContext in Infrastructure :

```
using Catalog.Domain;
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
            new Book { Id = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Price = 10.99m },
            new Book { Id = 2, Title = "1984", Author = "George Orwell", Price = 8.99m },
            new Book { Id = 3, Title = "To Kill a Mockingbird", Author = "Harper Lee", Price = 7.99m }
        );
    }
}

```

- [ ] Setup EF Core in Program.cs
```
builder.Services.AddDbContext<CatalogContext>(options =>
{
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
```
- [ ] appsettings.Development.json
```
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=localhost;Initial Catalog=MicroserviceCatalog;Integrated Security=True;Trust Server Certificate=true"
  },
```
- [ ] Add migrations & update DB