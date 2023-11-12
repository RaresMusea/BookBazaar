using BookBazaar.Data.DataContext;
using BookBazaar.Data.Repo.Interfaces;
using BookBazaar.Models.AppUser;

namespace BookBazaar.Data.Repo.Impl;

public class AppUserRepository : Repository<AppUser>, IAppUserRepository
{
    private readonly AppDataContext _context;

    public AppUserRepository(AppDataContext context) : base(context)
    {
        _context = context;
    }
}