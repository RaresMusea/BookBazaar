using BookBazaar.Models.CategoryModels;
using Microsoft.EntityFrameworkCore;

namespace BookBazaar.Data.DataContext;

public class AppDataContext : DbContext
{
    public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Genre = "Adventure", Priority = 1 },
            new Category { Id = 2, Genre = "Science-Fiction", Priority = 1 },
            new Category { Id = 3, Genre = "Psychology", Priority = 1 }
        );
    }
}