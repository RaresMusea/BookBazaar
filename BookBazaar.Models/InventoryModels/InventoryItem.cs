using System.ComponentModel.DataAnnotations;

namespace BookBazaar.Models.InventoryModels;

public class InventoryItem
{
    [Key] public int Id { get; set; }

    [Required] public int BookId { get; set; }

    [Required] public int QuantityInStock { get; set; }

    [Required] public int QuantitySold { get; set; }

    [Required] public DateTime DateAdded { get; set; }
}