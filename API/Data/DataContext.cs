using API.Entity;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Name = "Laptop",
                Description = "A high-performance laptop suitable for all your computing needs.",
                Price = 999.99m,
                ImageUrl = "https://example.com/images/laptop.png",
                IsActive = true,
                Stock = 50,
            },

            new Product
            {
                Id = 2,
                Name = "Smartphone",
                Description = "A sleek smartphone with the latest features and a stunning display.",
                Price = 699.99m,
                ImageUrl = "https://example.com/images/smartphone.png",
                IsActive = true,
                Stock = 150,
            },

            new Product
            {
                Id = 3,
                Name = "Wireless Headphones",
                Description = "Noise-cancelling wireless headphones for an immersive audio experience.",
                Price = 199.99m,
                ImageUrl = "https://example.com/images/headphones.png",
                IsActive = true,
                Stock = 80,
            }
        );
    }
}
