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

    [Required]
    [MinLength(3, ErrorMessage = "The book title should have at least 3 characters!")]
    [MaxLength(150, ErrorMessage = "The book title should have at most 150 characters!")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(150, ErrorMessage = "The name of the author(s) should contain at most 150 characters!")]
    public string Author { get; set; } = string.Empty;

    [Required] [ValidateNever] public string Description { get; set; } = string.Empty;

    [Required]
    [ValidateNever]
    [DisplayName("ISBN")]
    public string Isbn { get; set; } = string.Empty;

    [Required]
    [MaxLength(60, ErrorMessage = "The publisher name should contain at most 60 characters!")]
    public string Publisher { get; set; } = string.Empty;

    [Required]
    [MaxLength(25, ErrorMessage = "The language in which the book was written should contain at most 25 characters")]
    public string Language { get; set; } = string.Empty;

    [ValidateNever]
    [DisplayName("Book Cover")]
    public string CoverImageUrl { get; set; } = string.Empty;

    [Range(2.00, 600.00)]
    [DisplayName("Price (€)")]
    [Required]
    public double Price { get; set; }

    [DisplayName("Date published")]
    [ValidateNever]
    public DateTime DatePublished { get; set; }
}