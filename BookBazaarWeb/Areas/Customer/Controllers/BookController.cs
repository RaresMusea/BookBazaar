using BookBazaar.Data.Repo.Interfaces;
using BookBazaar.Models.BookModels;
using BookBazaar.Models.VM;
using Microsoft.AspNetCore.Mvc;

namespace BookBazaarWeb.Areas.Customer.Controllers;

[Area("Customer")]
public class BookController : Controller
{
    private readonly IWorkUnit _workUnit;

    public BookController(IWorkUnit workUnit)
    {
        _workUnit = workUnit;
    }

    public async Task<IActionResult> Index(string category = "All")
    {
        IEnumerable<Book> books = await _workUnit.BookRepo.RetrieveAllAsync(includedProperties: "Category");

        if (category != "All")
        {
            books = FilterBooksBasedOnCategory(category, books);
        }

        List<BookViewModel> viewModels = new();
        foreach (var book in books)
        {
            viewModels.Add(new()
            {
                Book = book,
                InventoryItem = await _workUnit.InventoryRepo.GetAsync(inv => inv.BookId == book.Id),
                CategoryQuery = category
            });
        }

        return View(viewModels.OrderByDescending(o => o.InventoryItem!.QuantityInStock).ToList());
    }

    public async Task<IActionResult> Search([FromQuery] string query)
    {
        if (string.IsNullOrEmpty(query))
        {
            return NotFound();
        }

        IEnumerable<Book> booksRelatedToQuery =
            await _workUnit.BookRepo.RetrieveAllAsync(b =>
                    b.Title.Contains(query) || b.Author.Contains(query) || b.Publisher.Contains(query) ||
                    b.Isbn.Contains(query) || b.Category!.Genre.Contains(query) || b.Language.Contains(query),
                includedProperties: "Category");

        List<BookViewModel> viewModels = new();

        foreach (var book in booksRelatedToQuery)
        {
            viewModels.Add(new()
            {
                Book = book,
                InventoryItem = await _workUnit.InventoryRepo.GetAsync(inv => inv.BookId == book.Id),
                SearchQuery = query
            });
        }

        return View(viewModels);
    }

    private IEnumerable<Book> FilterBooksBasedOnCategory(string category, IEnumerable<Book> books)
    {
        return books.Where(b => b.Category!.Genre == category);
    }
}