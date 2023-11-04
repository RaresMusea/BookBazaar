using BookBazaar.Data.DataContext;
using BookBazaar.Data.Repo.Impl;
using BookBazaar.Data.Repo.Interfaces;

namespace BookBazaar.Data.Repo.UnitOfWork;

public class WorkUnit : IWorkUnit
{
    private readonly AppDataContext _context;
    public ICategoryRepository CategoryRepo { get; private set; }
    public IBookRepository BookRepo { get; private set; }
    public IInventoryItemRepository InventoryRepo { get; private set; }
    public ICompanyRepository CompanyRepo { get; private set; }
    public IOrderBasketRepository OrderBasketRepo { get; private set; }

    public WorkUnit(AppDataContext context)
    {
        _context = context;
        CategoryRepo = new CategoryRepository(_context);
        BookRepo = new BookRepository(_context);
        InventoryRepo = new InventoryItemRepository(_context);
        CompanyRepo = new CompanyRepository(_context);
        OrderBasketRepo = new OrderBasketRepository(_context);
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
}