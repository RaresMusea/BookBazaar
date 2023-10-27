using BookBazaar.Data.Repo.Interfaces;
using BookBazaar.Models.BookModels;
using BookBazaar.Models.InventoryModels;
using BookBazaar.Models.VM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookBazaarWeb.Areas.Admin.Controllers;

[Area("Admin")]
public class BookController : Controller
{
    private readonly IWorkUnit _workUnit;

    public BookController(IWorkUnit workUnit)
    {
        _workUnit = workUnit;
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<Book> books = (await _workUnit.BookRepo.RetrieveAllAsync("Category"));

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

    public async Task<IActionResult> Create()
    {
        IEnumerable<SelectListItem> categories = await GetCategoriesAsListItemAsync();
        BookViewModel viewModel = new()
        {
            CategoriesList = categories,
            Book = new Book()
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(BookViewModel payload, IFormFile? bookCover)
    {
        if (payload.Book != null)
        {
            if (await _workUnit.BookRepo.Exists(payload.Book))
            {
                ModelState.AddModelError("", $"Unable to add book '{payload.Book.Title} by {payload.Book.Author}," +
                                             $" from {payload.Book.Publisher}' because another book with the same title," +
                                             $" written by the same author and published by the same publisher already exists!");
            }

            if (ModelState.IsValid)
            {
                await _workUnit.BookRepo.CreateAsync(payload.Book);
                payload.InventoryItem!.DateAdded = DateTime.Now;
                await _workUnit.InventoryRepo.CreateAsync(payload.InventoryItem!);
                await _workUnit.SaveAsync();
                TempData["SuccessfulOperation"] =
                    $"{payload.Book.Title} by {payload.Book.Author} was successfully added!";

                return RedirectToAction("Index");
            }
        }

        IEnumerable<SelectListItem> categories = await GetCategoriesAsListItemAsync();
        payload.CategoriesList = categories;
        return View(payload);
    }

    public async Task<IActionResult> Update(int? id)
    {
        if (id is null || id == 0)
        {
            return NotFound();
        }

        IEnumerable<SelectListItem> categories = await GetCategoriesAsListItemAsync();
        Book? book = await _workUnit.BookRepo.GetAsync(b => b.Id == id);
        InventoryItem inventoryItem = await _workUnit.InventoryRepo.GetAsync(i => i.BookId == id);

        if (book is null || inventoryItem is null || categories is null)
        {
            return NotFound();
        }

        BookViewModel viewModel = new()
        {
            Book = book,
            CategoriesList = categories,
            InventoryItem = inventoryItem,
        };

        return View("Update", viewModel);
    }

    [HttpPost, ActionName("Update")]
    public async Task<IActionResult> Update(BookViewModel payload)
    {
        if (payload.Book is null || payload.InventoryItem is null)
        {
            TempData["FailedOperation"] = "The book you were trying to update was not found!";
            return RedirectToAction("Index", NotFound());
        }

        if (ModelState.IsValid)
        {
            payload.InventoryItem!.DateUpdated = DateTime.Now;
            _workUnit.BookRepo.Update(payload.Book!);
            _workUnit.InventoryRepo.Update(payload.InventoryItem!);
            await _workUnit.SaveAsync();
            TempData["SuccessfulOperation"] = "Book entry updated successfully!";
            return RedirectToAction("Index");
        }

        TempData["FailedOperation"] = "Book entry could not be updated!";
        return View("Update", payload);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null || id == 0)
        {
            return NotFound();
        }

        Book? requestedBook = await _workUnit.BookRepo.GetAsync(book => book.Id == id);
        InventoryItem? requestedInventoryItem = await _workUnit.InventoryRepo.GetAsync(inv => inv.BookId == id);

        if (requestedBook is null || requestedInventoryItem is null)
        {
            TempData["FailedOperation"] = "Failed to delete the book because it cannot be found!";
            return RedirectToAction("Index", NotFound());
        }

        BookDeleteViewModel viewModel = new()
        {
            Book = requestedBook,
            InventoryItem = requestedInventoryItem,
        };

        return PartialView("_Delete", viewModel);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> Delete(BookDeleteViewModel payload)
    {
        if (ModelState.IsValid)
        {
            _workUnit.InventoryRepo.Remove(payload.InventoryItem!);
            _workUnit.BookRepo.Remove(payload.Book!);
            await _workUnit.SaveAsync();

            TempData["SuccessfulOperation"] = "Successfully deleted book entry!";
        }
        else
        {
            TempData["FailedOperation"] = "Deletion failed!";
        }

        return RedirectToAction("Index");
    }

    private async Task<IEnumerable<SelectListItem>> GetCategoriesAsListItemAsync()
    {
        IEnumerable<SelectListItem> categories = (await _workUnit.CategoryRepo.RetrieveAllAsync())
            .ToList().Select(
                elem => new SelectListItem
                {
                    Text = elem.Genre,
                    Value = elem.Id.ToString(),
                });

        return categories;
    }
}