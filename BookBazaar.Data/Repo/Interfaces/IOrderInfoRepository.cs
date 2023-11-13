using BookBazaar.Models.OrderModels;

namespace BookBazaar.Data.Repo.Interfaces;

public interface IOrderInfoRepository : IRepository<OrderInfo>
{
    OrderInfo Update(OrderInfo info);
}