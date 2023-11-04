using BookBazaar.Models.CartModels;

namespace BookBazaarWeb.Areas.Customer.Utils;

public static class OrderBasketControllerUtils
{
    public static double Savings { get; private set; }
    public static double TotalWithoutDiscount { get; private set; }
    public static double GrandTotal { get; private set; }

    public static Dictionary<int, double> ComputeDiscounts(IEnumerable<OrderBasket> orderBasketList)
    {
        Dictionary<int, double> result = new();

        foreach (OrderBasket orderBasket in orderBasketList)
        {
            int bookId = orderBasket.BookId;
            int amount = orderBasket.Items;
            double initialPrice = orderBasket.Book.Price * orderBasket.Items;
            double discount = 0.00;

            if (amount is >= 10 and < 25)
            {
                result[bookId] = 0.10;
                discount = 0.10;
            }
            else if (amount is >= 25 and < 50)
            {
                result[bookId] = 0.15;
                discount = 0.15;
            }
            else if (amount is >= 50)
            {
                result[bookId] = 0.30;
                discount = 0.30;
            }
            else
            {
                result[bookId] = 0.00;
            }

            Savings += initialPrice - discount * initialPrice;
            TotalWithoutDiscount += initialPrice;
            GrandTotal += discount * initialPrice;
        }

        return result;
    }
}