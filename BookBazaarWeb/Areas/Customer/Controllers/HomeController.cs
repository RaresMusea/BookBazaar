using System.Diagnostics;
using System.Security.Claims;
using BookBazaar.Data.Repo.Interfaces;
using BookBazaar.Misc.Session;
using BookBazaar.Models.BookModels;
using BookBazaar.Models.CartModels;
using BookBazaar.Models.ErrorModels;
using BookBazaar.Models.InventoryModels;
using BookBazaar.Models.VM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookBazaarWeb.Areas.Customer.Controllers;

[Area("Customer")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IWorkUnit _workUnit;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HomeController(ILogger<HomeController> logger, IWorkUnit workUnit, IHttpContextAccessor contextAccessor)
    {
        _logger = logger;
        _workUnit = workUnit;
        _httpContextAccessor = contextAccessor;
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<Book> books = await _workUnit.BookRepo.RetrieveAllAsync(includedProperties: "Category");
        IEnumerable<InventoryItem> inventoryItems =
            await _workUnit.InventoryRepo.RetrieveAllAsync(i => i.QuantityInStock > 0);
        inventoryItems = inventoryItems.OrderByDescending(i => i.QuantitySold).Take(4);

        if (books is null || inventoryItems is null)
        {
            return NotFound();
        }

        List<BookViewModel> viewModels = new();

        foreach (var inventoryItem in inventoryItems)
        {
            viewModels.Add(new()
            {
                Book = books.FirstOrDefault(b => b.Id == inventoryItem.BookId),
                InventoryItem = inventoryItem
            });
        }

        return View(viewModels.ToList());
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
        List<Book> similarBooks = (await _workUnit.BookRepo.RetrieveAllAsync(
            b => b.CategoryId == book.CategoryId && b.Id != book.Id,
            "Category")).ToList();

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
        if (!ModelState.IsValid)
        {
            TempData["FailedOperation"] = "An error occurred while processing your request. Please try again later.";
            return RedirectToAction(nameof(Index));
        }

        ClaimsIdentity claimsIdentity = (ClaimsIdentity)User.Identity!;
        string userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        viewModel.OrderBasket!.UserId = userId;
        OrderBasket basket = viewModel.OrderBasket;

        InventoryItem inventoryItem =
            await _workUnit.InventoryRepo.GetAsync(i => i.Id == basket.InventoryItemId && i.BookId == basket.BookId);

        if (basket.Items > inventoryItem.QuantityInStock)
        {
            ModelState.AddModelError(string.Empty,
                "The selected amount of books exceeds the quantity available in stock for that product!");
        }

        OrderBasket existingBasket = await _workUnit.OrderBasketRepo.GetAsync(b => b.UserId == basket.UserId
            && b.BookId == basket.BookId && b.InventoryItemId == basket.InventoryItemId);

        if (existingBasket is null)
        {
            await _workUnit.OrderBasketRepo.CreateAsync(basket);
            int basketAmount = (await _workUnit.OrderBasketRepo.RetrieveAllAsync(b => b.UserId == userId)).Count();
            await _workUnit.SaveAsync();
            _httpContextAccessor.HttpContext!.Session.SetInt32(SessionManager.OrderBasketSession, basketAmount);
        }
        else
        {
            existingBasket.Items += basket.Items;
            _workUnit.OrderBasketRepo.Update(existingBasket);
        }

        TempData["SuccessfulOperation"] = "Your cart was successfully updated!";
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