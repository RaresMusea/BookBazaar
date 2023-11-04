using BookBazaar.Data.DataContext;
using BookBazaar.Data.Repo.Interfaces;
using BookBazaar.Models.CartModels;
using Microsoft.EntityFrameworkCore;

namespace BookBazaar.Data.Repo.Impl;

public class OrderBasketRepository : Repository<OrderBasket>, IOrderBasketRepository
{
    private readonly AppDataContext _context;

    public OrderBasketRepository(AppDataContext context) : base(context)
    {
        _context = context;
    }

    public OrderBasket Update(OrderBasket basket)
    {
        return _context.OrderBaskets.Update(basket).Entity;
    }

    public async Task<bool> Exists(OrderBasket basket)
    {
        if (basket is null)
        {
            return false;
        }

        return await _context.OrderBaskets.FirstOrDefaultAsync(b =>
            b.Id == basket.Id && b.BookId == basket.BookId && b.UserId == basket.UserId) is not null;
    }
}