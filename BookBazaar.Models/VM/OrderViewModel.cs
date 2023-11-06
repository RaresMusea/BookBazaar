using BookBazaar.Models.OrderModels;

namespace BookBazaar.Models.VM;

public class OrderViewModel
{
    public Order Order { get; set; } = null!;
    public IEnumerable<OrderInfo> OrderInfos { get; set; } = null!;
}