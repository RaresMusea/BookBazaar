using BookBazaar.Models.CartModels;

namespace BookBazaar.Data.Repo.Interfaces;

public interface IOrderBasketRepository : IRepository<OrderBasket>
{
    OrderBasket Update(OrderBasket basket);
    Task<bool> Exists(OrderBasket basket);
}