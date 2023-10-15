using BookBazaar.Data.DataContext;
using BookBazaar.Data.Repo.Interfaces;
using BookBazaar.Models.CategoryModels;

namespace BookBazaar.Data.Repo;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly AppDataContext _context;

    public CategoryRepository(AppDataContext context) : base(context)
    {
        _context = context;
    }

    public Category Update(Category category)
    {
        return _context.Categories.Update(category).Entity;
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
}