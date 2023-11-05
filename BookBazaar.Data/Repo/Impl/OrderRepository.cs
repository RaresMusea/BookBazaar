using BookBazaar.Data.DataContext;
using BookBazaar.Data.Repo.Interfaces;
using BookBazaar.Models.OrderModels;

namespace BookBazaar.Data.Repo.Impl;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    private readonly AppDataContext _context;

    public OrderRepository(AppDataContext context) : base(context)
    {
        _context = context;
    }

    public Order Update(Order order)
    {
        return _context.Update(order).Entity;
    }
}