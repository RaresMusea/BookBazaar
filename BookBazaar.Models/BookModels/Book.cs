using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookBazaar.Models.BookModels;

public class Book
{
    [Key] public int Id { get; set; }

    [Required] public string Title { get; set; } = string.Empty;

    [Required] public string Author { get; set; } = string.Empty;

    [Required] public string Description { get; set; } = string.Empty;

    [Required] [DisplayName("ISBN")] public string Isbn { get; set; } = string.Empty;

    [Required] public string Publisher { get; set; } = string.Empty;

    [Required] public string Language { get; set; } = string.Empty;

    /*[Required] public byte[]? CoverImage { get; set; }*/

    [Range(2, 600)] [Required] public double Price { get; set; }

    [DisplayName("Date published")] public DateTime DatePublished { get; set; }
}