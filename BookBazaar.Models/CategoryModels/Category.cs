using System.ComponentModel.DataAnnotations;

namespace BookBazaar.Models.CategoryModels;

public class Category
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Genre is a required field!")]
    [MaxLength(30, ErrorMessage = "The genre should contain at most 30 characters!")]
    public string Genre { get; set; } = string.Empty;
}