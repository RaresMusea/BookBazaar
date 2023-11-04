using BookBazaar.Models.BookModels;
using BookBazaar.Models.CartModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookBazaar.Models.VM;

public class OrderBasketViewModel
{
    public OrderBasket? OrderBasket { get; set; } = null!;

    [ValidateNever] public IEnumerable<Book> SimilarSuggestions { get; set; } = null!;
}