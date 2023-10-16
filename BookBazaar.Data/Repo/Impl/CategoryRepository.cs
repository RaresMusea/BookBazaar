using BookBazaar.Data.DataContext;
using BookBazaar.Data.Repo.Interfaces;
using BookBazaar.Models.CategoryModels;

namespace BookBazaar.Data.Repo.Impl;

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
}