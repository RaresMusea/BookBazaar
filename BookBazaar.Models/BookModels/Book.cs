using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BookBazaar.Models.CategoryModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookBazaar.Models.BookModels;

public class Book
{
    [Key] public int Id { get; set; }

    [DisplayName("Genre")] public int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    [ValidateNever]
    public Category? Category { get; set; }

    [Required] public string Title { get; set; } = string.Empty;

    [Required] public string Author { get; set; } = string.Empty;

    [Required] public string Description { get; set; } = string.Empty;

    [Required] [DisplayName("ISBN")] public string Isbn { get; set; } = string.Empty;

    [Required] public string Publisher { get; set; } = string.Empty;

    [Required] public string Language { get; set; } = string.Empty;

    [ValidateNever] public string CoverImageUrl { get; set; } = string.Empty;

    [Range(2, 600)] [Required] public double Price { get; set; }

    [DisplayName("Date published")] public DateTime DatePublished { get; set; }
}