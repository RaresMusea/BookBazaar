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
        if (id is null || id == 0)
        {
            return NotFound();
        }

        Category category = (await _context.Categories.FirstOrDefaultAsync(c => c.Id == id));

        if (category is null)
        {
            return NotFound();
        }

        return View("Update", category);
    }

    [HttpPost]
    public IActionResult Update(Category categorPayload)
    {
        if (!ModelState.IsValid)
        {
            return View("Update", categorPayload);
        }

        _context.Categories.Update(categorPayload);
        _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}