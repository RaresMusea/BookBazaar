using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BookBazaar.Models.BookModels;
using BookBazaar.Models.InventoryModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookBazaar.Models.OrderModels;

public class OrderInfo
{
    [Key] public int Id { get; set; }

    [Required] public int OrderId { get; set; }

    [ValidateNever]
    [ForeignKey("OrderId")]
    public Order Order { get; set; } = null!;

    [Required] public int BookId { get; set; }

    [ValidateNever] [ForeignKey("BookId")] public Book Book { get; set; } = null!;

    [Required] public int InventoryItemId { get; set; }

    [ValidateNever]
    [ForeignKey("InventoryItemId")]
    public InventoryItem InventoryItem { get; set; } = null!;

    public int Amount { get; set; }

    public double Discount { get; set; }

    public double Price { get; set; }
}