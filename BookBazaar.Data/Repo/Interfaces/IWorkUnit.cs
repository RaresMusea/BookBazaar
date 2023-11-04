namespace BookBazaar.Data.Repo.Interfaces;

public interface IWorkUnit
{
    ICategoryRepository CategoryRepo { get; }
    IBookRepository BookRepo { get; }
    IInventoryItemRepository InventoryRepo { get; }
    ICompanyRepository CompanyRepo { get; }
    IOrderBasketRepository OrderBasketRepo { get; }
    IAppUserRepository UserRepo { get; }

    Task<int> SaveAsync();
}