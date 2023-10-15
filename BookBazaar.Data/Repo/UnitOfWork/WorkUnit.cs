using BookBazaar.Data.DataContext;
using BookBazaar.Data.Repo.Interfaces;

namespace BookBazaar.Data.Repo.UnitOfWork;

public class WorkUnit : IWorkUnit
{
    private readonly AppDataContext _context;
    public ICategoryRepository CategoryRepo { get; private set; }


    public WorkUnit(AppDataContext context)
    {
        _context = context;
        CategoryRepo = new CategoryRepository(_context);
    }

    public Task<int> SaveAsync()
    {
        return _context.SaveChangesAsync();
    }
}