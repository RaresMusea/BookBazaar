using BookBazaar.Models.CartModels;
using BookBazaar.Models.OrderModels;

namespace BookBazaar.Models.VM;

public class DetailedOrderBasketViewModel
{
    public IEnumerable<OrderBasket> OrderBasketList { get; set; } = null!;
    public Order Order { get; set; } = null!;
    public Dictionary<int, double> BookDiscounts { get; set; } = null!;
}