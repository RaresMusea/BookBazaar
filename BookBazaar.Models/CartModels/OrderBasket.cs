using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BookBazaar.Misc.CustomValidations;
using BookBazaar.Models.BookModels;
using BookBazaar.Models.InventoryModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookBazaar.Models.CartModels;

public class OrderBasket
{
    [Key] public int Id { get; set; }

    public int BookId { get; set; }

    [ForeignKey("BookId")] [ValidateNever] public Book Book { get; set; } = null!;

    public int InventoryItemId { get; set; }

    [ForeignKey("InventoryItemId")]
    [ValidateNever]
    public InventoryItem InventoryEntry { get; set; } = null!;

    [CustomRange(1, 100, "InventoryEntry.QuantityInStock",
        ErrorMessage = "You selected a quantity which exceeds the available stock for this product!")]
    public int Items { get; set; }

    public string UserId { get; set; } = string.Empty;

    [ForeignKey("UserId")] [ValidateNever] public AppUser User { get; set; } = null!;
}