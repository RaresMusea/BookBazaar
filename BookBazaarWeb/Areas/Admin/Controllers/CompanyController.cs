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

    [HttpPost]
    public async Task<IActionResult> Create(Company company)
    {
        if (await _workUnit.CompanyRepo.Exists(company))
        {
            ModelState.AddModelError("", $"Unable to create company '{company.Name}'" +
                                         $" because another company with the same details already exists!");
        }

        if (ModelState.IsValid)
        {
            await _workUnit.CompanyRepo.CreateAsync(company);
            await _workUnit.SaveAsync();
            TempData["SuccessfulOperation"] = $"{company.Name} category was created successfully!";
            return RedirectToAction("Index");
        }

        return View();
    }

    public async Task<IActionResult> Update(int? id)
    {
        if (id <= 0 || id is null)
        {
            NotFound();
        }

        Company? company = await _workUnit.CompanyRepo.GetAsync(comp => comp.Id == id);

        if (company is null)
        {
            return NotFound();
        }

        return View("Update", company);
    }

    [HttpPost, ActionName("Update")]
    public async Task<IActionResult> Update(Company companyPayload)
    {
        if (!ModelState.IsValid)
        {
            TempData["FailedOperation"] = "The category could not be updated";
            return View("Update", companyPayload);
        }

        _workUnit.CompanyRepo.Update(companyPayload);
        await _workUnit.SaveAsync();
        TempData["SuccessfulOperation"] = "The associated company was updated successfully!";
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null || id == 0)
        {
            return NotFound();
        }

        Company? requestedCompany = await _workUnit.CompanyRepo.GetAsync(cat => cat.Id == id);

        if (requestedCompany is null)
        {
            return NotFound();
        }

        return PartialView("_Delete", requestedCompany);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> Delete(Company companyPayload)
    {
        _workUnit.CompanyRepo.Remove(companyPayload);
        await _workUnit.SaveAsync();
        return RedirectToAction("Index");
    }
}