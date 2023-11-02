using BookBazaar.Data.Repo.Interfaces;
using BookBazaar.Misc;
using BookBazaar.Models.CompanyModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookBazaarWeb.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = RoleManager.Administrator)]
public class CompanyController : Controller
{
    private readonly IWorkUnit _workUnit;

    public CompanyController(IWorkUnit workUnit)
    {
        _workUnit = workUnit;
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<Company> companies = await _workUnit.CompanyRepo.RetrieveAllAsync();
        return View(companies);
    }

    public IActionResult Create()
    {
        return View();
    }
}