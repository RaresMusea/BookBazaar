using System.Reflection;
using AutoMapper;
using BookBazaar.Data.Repo.Interfaces;
using BookBazaar.Misc;
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
    private readonly IMapper _mapper;

    public BookController(IWorkUnit workUnit, IMapper mapper)
    {
        _workUnit = workUnit;
        _mapper = mapper;
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

    private void UpdateModelState(ref BookViewModel payload, BookViewModel existingResource)
    {
        PropertyInfo[] payloadProperties = payload.GetType().GetProperties();

        foreach (PropertyInfo info in payloadProperties)
        {
            object? modifiedFieldValue = info.GetValue(payload);
            object? existingFieldValue = existingResource.GetType().GetProperty(info.Name)!.GetValue(existingResource);

            if (modifiedFieldValue is null && !existingFieldValue!.Equals(modifiedFieldValue))
            {
                modifiedFieldValue!.GetType().GetProperty(info.Name)!.SetValue(payload, existingFieldValue);
            }
        }
    }
}