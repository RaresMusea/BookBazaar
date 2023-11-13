using BookBazaar.Models.CartModels;

namespace BookBazaarWeb.Areas.Customer.Utils;

public class OrderBasketControllerUtils
{
    public double Savings { get; private set; }

    public double TotalWithoutDiscount { get; private set; }

    public double GrandTotal { get; private set; }

    public int TotalProducts { get; private set; }

    public bool DiscountsApplied { get; private set; }

    public Dictionary<int, double> ComputeDiscounts(IEnumerable<OrderBasket> orderBasketList)
    {
        Dictionary<int, double> result = new();

        foreach (OrderBasket orderBasket in orderBasketList)
        {
            int bookId = orderBasket.BookId;
            int amount = orderBasket.Items;
            double initialPrice = (orderBasket.Book.Price * orderBasket.Items);
            double discount = 0.00;

            if (amount is >= 10 and < 25)
            {
                result[bookId] = 0.10;
                discount = Math.Round(0.10, 2);
                DiscountsApplied = true;
            }
            else if (amount is >= 25 and < 50)
            {
                result[bookId] = 0.15;
                discount = Math.Round(0.15, 2);
                DiscountsApplied = true;
            }
            else if (amount is >= 50)
            {
                result[bookId] = 0.30;
                discount = Math.Round(0.30, 2);
                DiscountsApplied = true;
            }
            else
            {
                result[bookId] = 0.00;
            }

            Savings += (discount * initialPrice);
            TotalWithoutDiscount += initialPrice;
            GrandTotal += initialPrice - ((discount >= 0.0 ? discount : 1) * initialPrice);
            TotalProducts += amount;
        }

        Savings = Math.Round(Savings, 2);
        TotalWithoutDiscount = Math.Round(TotalWithoutDiscount, 2);
        GrandTotal = Math.Round(GrandTotal, 2);

        return result;
    }
}