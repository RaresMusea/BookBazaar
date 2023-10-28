using BookBazaar.Data.DataContext;
using BookBazaar.Data.Repo.Interfaces;
using BookBazaar.Models.InventoryModels;

namespace BookBazaar.Data.Repo.Impl;

public class InventoryItemRepository : Repository<InventoryItem>, IInventoryItemRepository
{
    private readonly AppDataContext _context;
    
    public InventoryItemRepository(AppDataContext context) : base(context)
    {
        _context = context;
    }

    public InventoryItem Update(InventoryItem inventoryItem)
    {
        return _context.InventoryItems.Update(inventoryItem).Entity;
    }
}