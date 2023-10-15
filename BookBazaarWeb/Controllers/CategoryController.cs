using BookBazaar.Data.Repo.Interfaces;
using BookBazaar.Models.CategoryModels;
using Microsoft.AspNetCore.Mvc;

namespace BookBazaarWeb.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryController(ICategoryRepository repository)
    {
        _categoryRepository = repository;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _categoryRepository.RetrieveAllAsync();
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
            await _categoryRepository.CreateAsync(category);
            int affectedRows = await _categoryRepository.SaveAsync();
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

        Category? category = await _categoryRepository.GetAsync(cat => cat.Id == id);

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

        _categoryRepository.Update(categoryPayload);
        await _categoryRepository.SaveAsync();
        TempData["SuccessfulOperation"] = "Category updated successfully!";
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null || id == 0)
        {
            return NotFound();
        }

        Category? requestedCategory = await _categoryRepository.GetAsync(cat => cat.Id == id);

        if (requestedCategory is null)
        {
            return NotFound();
        }

        return PartialView("_Delete", requestedCategory);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> Delete(Category categoryPayload)
    {
        _categoryRepository.Remove(categoryPayload);
        await _categoryRepository.SaveAsync();
        return RedirectToAction("Index");
    }
}