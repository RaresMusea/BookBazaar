using BookBazaar.Data.Repo.Interfaces;
using BookBazaar.Models.CategoryModels;
using Microsoft.AspNetCore.Mvc;

namespace BookBazaarWeb.Controllers;

public class CategoryController : Controller
{
    private readonly IWorkUnit _workUnit;

    public CategoryController(IWorkUnit workUnit)
    {
        _workUnit = workUnit;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _workUnit.CategoryRepo.RetrieveAllAsync();
        return View(categories.ToList());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Category category)
    {
        if (ModelState.IsValid)
        {
            await _workUnit.CategoryRepo.CreateAsync(category);
            int affectedRows = await _workUnit.SaveAsync();
            TempData["SuccessfulOperation"] = $"{category.Genre} category was created successfully!";
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

        Category? category = await _workUnit.CategoryRepo.GetAsync(cat => cat.Id == id);

        if (category is null)
        {
            return NotFound();
        }

        return View("Update", category);
    }

    [HttpPost]
    public async Task<IActionResult> Update(Category categoryPayload)
    {
        if (!ModelState.IsValid)
        {
            return View("Update", categoryPayload);
        }

        _workUnit.CategoryRepo.Update(categoryPayload);
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

        Category? requestedCategory = await _workUnit.CategoryRepo.GetAsync(cat => cat.Id == id);

        if (requestedCategory is null)
        {
            return NotFound();
        }

        return PartialView("_Delete", requestedCategory);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> Delete(Category categoryPayload)
    {
        _workUnit.CategoryRepo.Remove(categoryPayload);
        await _workUnit.SaveAsync();
        return RedirectToAction("Index");
    }
}