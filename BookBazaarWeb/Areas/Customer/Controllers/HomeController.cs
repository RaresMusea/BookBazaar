using System.Diagnostics;
using System.Security.Claims;
using BookBazaar.Data.Repo.Interfaces;
using BookBazaar.Models.BookModels;
using BookBazaar.Models.CartModels;
using BookBazaar.Models.InventoryModels;
using BookBazaar.Models.VM;
using BookBazaarWeb.Models;
using Microsoft.AspNetCore.Authorization;
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

            return View(viewModels.OrderByDescending(o => o.InventoryItem!.QuantityInStock).ToList());
        }

        return NotFound();
    }

    public async Task<IActionResult> Details(int id)
    {
        if (id <= 0)
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

        OrderBasket basket = new()
        {
            Book = book,
            InventoryItem = inventoryItem,
            InventoryItemId = inventoryItem.Id,
            Items = 1,
            BookId = id,
        };

        OrderBasketViewModel viewModel = new()
        {
            OrderBasket = basket,
            SimilarSuggestions = similarBooks,
        };

        return View(viewModel);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Details(OrderBasketViewModel viewModel)
    {
        ClaimsIdentity claimsIdentity = (ClaimsIdentity)User.Identity!;
        string userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        viewModel.OrderBasket!.UserId = userId;
        OrderBasket basket = viewModel.OrderBasket;

        OrderBasket existingBasket = await _workUnit.OrderBasketRepo.GetAsync(b => b.UserId == basket.UserId
            && b.BookId == basket.BookId && b.InventoryItemId == basket.InventoryItemId);

        if (existingBasket is null)
        {
            await _workUnit.OrderBasketRepo.CreateAsync(basket);
        }
        else
        {
            existingBasket.Items += basket.Items;
            _workUnit.OrderBasketRepo.Update(existingBasket);
        }

        await _workUnit.SaveAsync();
        return RedirectToAction(nameof(Index));
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