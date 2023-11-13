using BookBazaar.Data.DataContext;
using BookBazaar.Data.Repo.Interfaces;
using BookBazaar.Models.OrderModels;
using Microsoft.EntityFrameworkCore;

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

    public async Task UpdateOrderStateAsync(int id, string orderState, string? paymentState = null)
    {
        Order order = (await _context.Orders.FirstOrDefaultAsync(o => o.Id == id))!;

        if (order is not null)
        {
            order.OrderState = orderState;

            if (string.IsNullOrEmpty(order.TransactionState))
            {
                order.TransactionState = paymentState;
            }

            if (!string.IsNullOrEmpty(order.TransactionState) && !string.IsNullOrEmpty(paymentState) &&
                paymentState != order.TransactionState)
            {
                order.TransactionState = paymentState;
            }
        }
    }

    public async Task UpdateStripeIdAsync(int id, string sessId, string transactionId)
    {
        Order order = (await _context.Orders.FirstOrDefaultAsync(o => o.Id == id))!;

        if (!string.IsNullOrEmpty(sessId))
        {
            order.SessionId = sessId;
        }

        if (!string.IsNullOrEmpty(transactionId))
        {
            order.TransactionId = transactionId;
            order.PaymentDate = DateTime.Now;
        }
    }
}