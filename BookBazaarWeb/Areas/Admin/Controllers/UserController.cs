using BookBazaar.Data.Repo.Interfaces;
using BookBazaar.Misc.Roles;
using BookBazaar.Models.AppUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookBazaarWeb.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = RoleManager.Administrator)]
public class UserController : Controller
{
    private readonly IWorkUnit _workUnit;

    public UserController(IWorkUnit workUnit)
    {
        _workUnit = workUnit;
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<AppUser> users = await _workUnit.UserRepo.RetrieveAllAsync(includedProperties: "Company");
        return View(users);
    }
}