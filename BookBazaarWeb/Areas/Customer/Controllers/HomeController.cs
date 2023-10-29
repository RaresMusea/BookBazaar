using System.Diagnostics;
using BookBazaar.Data.Repo.Interfaces;
using BookBazaar.Models.BookModels;
using BookBazaar.Models.InventoryModels;
using BookBazaar.Models.VM;
using BookBazaarWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookBazaarWeb.Areas.Customer.Controllers;

[Area("Customer")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IWorkUnit _workUnit;

    public HomeController(ILogger<HomeController> logger, IWorkUnit workUnit)
    {
        _logger = logger;
        _workUnit = workUnit;
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<Book> books = await _workUnit.BookRepo.RetrieveAllAsync(includedProperties: "Category");
        if (books is not null)
        {
            List<BookViewModel> viewModels = new();
            foreach (var book in books)
            {
                viewModels.Add(new()
                {
                    Book = book,
                    InventoryItem = await _workUnit.InventoryRepo.GetAsync(inv => inv.BookId == book.Id)
                });
            }

            return View(viewModels);
        }

        return NotFound();
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Book? book = await _workUnit.BookRepo.GetAsync(b => b.Id == id, "Category");

        if (book is null)
        {
            return NotFound();
        }

        InventoryItem inventoryItem = await _workUnit.InventoryRepo.GetAsync(i => i.BookId == id);
        IEnumerable<Book> similarBooks = await _workUnit.BookRepo.RetrieveAllAsync(
            b => b.CategoryId == book.CategoryId && b.Id != book.Id,
            "Category");

        BookDetailsViewModel viewModel = new()
        {
            Book = book,
            InventoryItem = inventoryItem,
            SimilarSuggestions = similarBooks,
        };

        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}