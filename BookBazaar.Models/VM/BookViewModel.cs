using BookBazaar.Models.BookModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookBazaar.Models.VM;

public class BookViewModel
{
    public Book? Book { get; set; }
    [ValidateNever] public IEnumerable<SelectListItem>? CategoriesList { get; set; }
}