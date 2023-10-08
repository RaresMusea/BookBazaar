using Microsoft.AspNetCore.Mvc;

namespace BookBazaarWeb.Controllers;

[ApiController]
public class CategoryController : Controller
{
    [Route("{controller}")]
    public IActionResult Index()
    {
        return View();
    }
}