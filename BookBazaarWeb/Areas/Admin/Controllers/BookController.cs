using BookBazaar.Data.Repo.Interfaces;
using BookBazaar.Models.BookModels;
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
        var books = await _workUnit.BookRepo.RetrieveAllAsync();
        return View(books.ToList());
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

        if (book is null)
        {
            return NotFound();
        }

        BookViewModel viewModel = new()
        {
            Book = book,
            CategoriesList = categories,
        };

        return View("Update", viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Update(BookViewModel payload)
    {
        if (!ModelState.IsValid)
        {
            return View("Update", payload);
        }

        if (payload.Book is not null)
        {
            _workUnit.BookRepo.Update(payload.Book);
            await _workUnit.SaveAsync();
            TempData["SuccessfulOperation"] = "Book entry updated successfully!";
        }
        else
        {
            TempData["FailedOperation"] = "Cannot update book entry because it cannot be found!";
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null || id == 0)
        {
            return NotFound();
        }

        Book? requestedBook = await _workUnit.BookRepo.GetAsync(book => book.Id == id);

        if (requestedBook is null)
        {
            return NotFound();
        }

        return PartialView("_Delete", requestedBook);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> Delete(Book bookPayload)
    {
        _workUnit.BookRepo.Remove(bookPayload);
        await _workUnit.SaveAsync();
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