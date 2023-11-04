using BookBazaar.Models.CartModels;

namespace BookBazaar.Models.VM;

public class DetailedOrderBasketViewModel
{
    public IEnumerable<OrderBasket> OrderBasketList { get; set; }
    public double Total { get; set; }
    public Dictionary<int, double> BookDiscounts { get; set; }
}