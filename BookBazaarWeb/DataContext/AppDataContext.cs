using Microsoft.EntityFrameworkCore;

namespace BookBazaarWeb.DataContext;

public class AppDataContext : DbContext
{
    public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
    {
    }
}