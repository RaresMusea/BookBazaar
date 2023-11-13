using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookBazaar.Models.InventoryModels;

public class InventoryItem
{
    [Key] public int Id { get; set; }

    [ValidateNever] public int BookId { get; set; }

    [Required]
    [DisplayName("Quantity in stock")]
    public int QuantityInStock { get; set; }

    [Required] public int QuantitySold { get; set; }

    public DateTime DateAdded { get; set; }

    public DateTime? DateUpdated { get; set; }
}