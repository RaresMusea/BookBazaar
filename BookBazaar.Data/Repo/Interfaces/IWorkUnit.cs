namespace BookBazaar.Data.Repo.Interfaces;

public interface IWorkUnit
{
    ICategoryRepository CategoryRepo { get; }
    IBookRepository BookRepo { get; }
    IInventoryItemRepository InventoryRepo { get; }

    Task<int> SaveAsync();
}