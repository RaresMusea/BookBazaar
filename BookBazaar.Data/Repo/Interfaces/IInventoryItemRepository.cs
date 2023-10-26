using BookBazaar.Models.InventoryModels;

namespace BookBazaar.Data.Repo.Interfaces;

public interface IInventoryItemRepository : IRepository<InventoryItem>
{
    InventoryItem Update(InventoryItem inventoryItem);
}