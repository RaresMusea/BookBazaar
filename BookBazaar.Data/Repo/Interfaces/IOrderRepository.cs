using BookBazaar.Models.OrderModels;

namespace BookBazaar.Data.Repo.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    Order Update(Order order);
}