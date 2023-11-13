using BookBazaar.Data.DataContext;
using BookBazaar.Data.Repo.Interfaces;
using BookBazaar.Models.OrderModels;

namespace BookBazaar.Data.Repo.Impl;

public class OrderInfoRepository : Repository<OrderInfo>, IOrderInfoRepository
{
    private readonly AppDataContext _context;

    public OrderInfoRepository(AppDataContext context) : base(context)
    {
        _context = context;
    }

    public OrderInfo Update(OrderInfo info)
    {
        return _context.Update(info).Entity;
    }
}