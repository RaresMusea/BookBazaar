using BookBazaar.Models.BookModels;
using BookBazaar.Models.InventoryModels;

namespace BookBazaar.Models.VM;

public class BookDetailsViewModel
{
    public Book? Book { get; set; } = null!;
    public InventoryItem? InventoryItem { get; set; } = null!;
    public IEnumerable<Book> SimilarSuggestions { get; set; } = null!;
}