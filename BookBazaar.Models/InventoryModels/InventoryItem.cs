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
    [Range(1, 900, ErrorMessage = "The quantity should be an integer value, from 1 to 900!")]
    public int QuantityInStock { get; set; }

    [Required] public int QuantitySold { get; set; }

    public DateTime DateAdded { get; set; }

    public DateTime? DateUpdated { get; set; }
}