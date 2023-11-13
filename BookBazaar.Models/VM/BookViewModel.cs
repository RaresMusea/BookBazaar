using BookBazaar.Models.BookModels;
using BookBazaar.Models.InventoryModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookBazaar.Models.VM;

public class BookViewModel
{
    public Book? Book { get; set; }
    [ValidateNever] public IEnumerable<SelectListItem>? CategoriesList { get; set; }
    [ValidateNever] public InventoryItem? InventoryItem { get; set; }
    [ValidateNever] public string CategoryQuery { get; init; } = string.Empty;

    [ValidateNever] public string SearchQuery { get; init; } = string.Empty;
}