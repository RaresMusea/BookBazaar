using BookBazaar.Models.CategoryModels;

namespace BookBazaar.Data.Repo.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    Category Update(Category category);
    Task<int> SaveAsync();
}