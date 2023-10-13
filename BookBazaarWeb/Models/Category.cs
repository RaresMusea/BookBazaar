using System.ComponentModel.DataAnnotations;

namespace BookBazaarWeb.Models;

public class Category
{
    public int Id { get; set; }

    [Range(1, 100, ErrorMessage = "The display priority should be a numerical value within range 1-100!")]
    public int Priority { get; set; }

    [Required(ErrorMessage = "Genre is a required field!")]
    [MaxLength(30, ErrorMessage = "The genre should contain at most 30 characters!")]
    public string Genre { get; set; } = string.Empty;
}