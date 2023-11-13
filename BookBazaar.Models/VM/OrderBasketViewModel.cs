using BookBazaar.Models.BookModels;
using BookBazaar.Models.CartModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookBazaar.Models.VM;

public class OrderBasketViewModel
{
    public OrderBasket? OrderBasket { get; set; }

    [ValidateNever] public List<Book> SimilarSuggestions { get; set; } = null!;
}