using BookBazaar.Data.Repo.Interfaces;
using BookBazaar.Models.BookModels;
using Microsoft.AspNetCore.Mvc;

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

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Book book)
    {
        if (await _workUnit.BookRepo.Exists(book))
        {
            ModelState.AddModelError("", $"Unable to add book '{book.Title} by {book.Author}, from {book.Publisher}'" +
                                         $" because another book with the same title, written by the same author and published" +
                                         $"by the same publisher already exists!");
        }

        if (ModelState.IsValid)
        {
            await _workUnit.BookRepo.CreateAsync(book);
            await _workUnit.SaveAsync();
            TempData["SuccessfulOperation"] = $"{book.Title} by {book.Author} was successfully added!";
            return RedirectToAction("Index");
        }

        return View();
    }

    public async Task<IActionResult> Update(int? id)
    {
        if (id is null || id == 0)
        {
            return NotFound();
        }

        Book? book = await _workUnit.BookRepo.GetAsync(b => b.Id == id);

        if (book is null)
        {
            return NotFound();
        }

        return View("Update", book);
    }

    [HttpPost]
    public async Task<IActionResult> Update(Book bookPayload)
    {
        if (!ModelState.IsValid)
        {
            return View("Update", bookPayload);
        }

        _workUnit.BookRepo.Update(bookPayload);
        await _workUnit.SaveAsync();
        TempData["SuccessfulOperation"] = "Category updated successfully!";
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
}