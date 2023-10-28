using BookBazaar.Models.BookModels;
using BookBazaar.Models.InventoryModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookBazaar.Models.VM;

public class BookDeleteViewModel
{
    public Book? Book { get; set; }
    [ValidateNever] public InventoryItem? InventoryItem { get; set; }
}