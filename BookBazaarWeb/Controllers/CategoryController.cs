using BookBazaarWeb.DataContext;
using BookBazaarWeb.Models;
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
            return RedirectToAction("Index");
        }

        return View();
    }

    public async Task<IActionResult> Update(int? id)
    {
        if (!id.HasValue)
        {
            return NotFound();
        }

        Category foundCategory = await _context.Categories.FirstOrDefaultAsync(cat => cat.Id == id);

        if (foundCategory is null)
        {
            return NotFound();
        }


        return PartialView("_Update", foundCategory);
    }
}