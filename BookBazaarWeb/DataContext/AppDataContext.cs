using BookBazaarWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BookBazaarWeb.DataContext;

public class AppDataContext : DbContext
{
    public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Genre = "Adventure", Priority = 1 },
            new Category { Id = 2, Genre = "Science-Fiction", Priority = 1 },
            new Category { Id = 3, Genre = "Psychology", Priority = 1 }
        );
    }
}