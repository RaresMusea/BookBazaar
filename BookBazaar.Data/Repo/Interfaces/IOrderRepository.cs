using BookBazaar.Models.OrderModels;

namespace BookBazaar.Data.Repo.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    Order Update(Order order);
    Task UpdateOrderStateAsync(int id, string orderState, string? paymentState = null);
    Task UpdateStripeIdAsync(int id, string sessId, string transactionId);
}