using BookBazaarWeb.DataContext;
using Microsoft.AspNetCore.Mvc;

namespace BookBazaarWeb.Controllers;

[ApiController]
public class CategoryController : Controller
{
    private readonly AppDataContext _context;

    public CategoryController(AppDataContext context)
    {
        _context = context;
    }

    [Route("{controller}")]
    public IActionResult Index()
    {
        var categoryList = _context.Categories.ToList();
        return View(categoryList);
    }
}