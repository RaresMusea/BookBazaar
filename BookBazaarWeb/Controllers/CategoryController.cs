using BookBazaar.Data.DataContext;
using BookBazaar.Models.CategoryModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookBazaarWeb.Controllers;

public class CategoryController : Controller
{
    private readonly AppDataContext _context;

    public CategoryController(AppDataContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var categoryList = await _context.Categories.ToListAsync();
        return View(categoryList);
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
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
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

        Category? category = (await _context.Categories.FirstOrDefaultAsync(c => c.Id == id));

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

        _context.Categories.Update(categoryPayload);
        await _context.SaveChangesAsync();
        TempData["SuccessfulOperation"] = "Category updated successfully!";
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null || id == 0)
        {
            return NotFound();
        }

        Category? requestedCategory = await _context.Categories.FirstOrDefaultAsync(cat => cat.Id == id);

        if (requestedCategory is null)
        {
            return NotFound();
        }

        return PartialView("_Delete", requestedCategory);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> Delete(Category categoryPayload)
    {
        _context.Categories.Remove(categoryPayload);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpDelete("/Remove{categoryId}")]
    public async Task<IActionResult> Remove(int? categoryId)
    {
        Category? requestedCategory = await _context.Categories.FirstOrDefaultAsync(cat => cat.Id == categoryId);

        if (requestedCategory is null)
        {
            return NotFound();
        }

        _context.Remove(requestedCategory);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}