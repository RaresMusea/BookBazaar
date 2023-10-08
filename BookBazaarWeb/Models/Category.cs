using System.ComponentModel.DataAnnotations;

namespace BookBazaarWeb.Models;

public class Category
{
    public int Id { get; set; }

    public int Priority { get; set; }

    [Required] public string Genre { get; set; } = string.Empty;
}