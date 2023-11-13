using BookBazaar.Data.DataContext;
using BookBazaar.Data.Repo.Interfaces;
using BookBazaar.Misc.Roles;
using BookBazaar.Models.AppUser;
using BookBazaar.Models.VM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookBazaarWeb.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = RoleManager.Administrator)]
public class UserController : Controller
{
    private readonly IWorkUnit _workUnit;
    private readonly AppDataContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public UserController(IWorkUnit workUnit, AppDataContext context, UserManager<IdentityUser> userManager)
    {
        _workUnit = workUnit;
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<AppUser> users = await _workUnit.UserRepo.RetrieveAllAsync(includedProperties: "Company");

        var userRoles = await _context.UserRoles.ToListAsync();
        IList<IdentityRole> roles = await _context.Roles.ToListAsync();
        IList<UserViewModel> viewModels = new List<UserViewModel>();

        foreach (var user in users)
        {
            string roleIdentifier = userRoles.FirstOrDefault(r => r.UserId == user.Id)!.RoleId;
            string? role = roles.FirstOrDefault(r => r.Id == roleIdentifier)!.Name;

            viewModels.Add(
                new UserViewModel
                {
                    User = user,
                    Role = role,
                });
        }

        return View(viewModels);
    }

    public async Task<IActionResult> Suspend(string userId)
    {
        var specificUser = await _workUnit.UserRepo.GetAsync(u => u.Id == userId);

        if (specificUser is null)
        {
            return NotFound();
        }

        if (specificUser.LockoutEnd is null || specificUser.LockoutEnd < DateTime.Now)
        {
            specificUser.LockoutEnd = DateTime.Now.AddYears(100);
        }

        await _workUnit.SaveAsync();
        TempData["SuccessfulOperation"] = $"User {specificUser.Name} was suspended.";

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Unsuspend(string userId)
    {
        var specificUser = await _workUnit.UserRepo.GetAsync(u => u.Id == userId);

        if (specificUser is null)
        {
            return NotFound();
        }

        if (specificUser.LockoutEnd is not null && specificUser.LockoutEnd > DateTime.Now)
        {
            specificUser.LockoutEnd = DateTime.Now;
        }

        await _workUnit.SaveAsync();
        TempData["SuccessfulOperation"] = $"User {specificUser.Name} was unsuspended";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> ManageUser(string userId)
    {
        string roleId = (await _context.UserRoles.FirstOrDefaultAsync(r => r.UserId == userId))!.RoleId;

        UserViewModel userViewModel = new()
        {
            User = await _workUnit.UserRepo.GetAsync(u => u.Id == userId, "Company"),
            Role = ((await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId))!).Name
        };

        UserManagementVewModel managementVewModel = new()
        {
            UserDetails = userViewModel,
            AvailableAppRoles = _context.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Name
            }),
            Companies = _context.Companies.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString(),
            })
        };

        return View(managementVewModel);
    }

    [HttpPost]
    public async Task<IActionResult> RoleManagement(UserManagementVewModel viewModel)
    {
        string roleId =
            ((await _context.UserRoles.FirstOrDefaultAsync(u => u.UserId == viewModel.UserDetails.User.Id))!)
            .RoleId;

        string previousRoleValue = ((await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId))!).Name!;

        if (viewModel.UserDetails.Role != previousRoleValue)
        {
            AppUser user = await _workUnit.UserRepo.GetAsync(u => u.Id == viewModel.UserDetails.User.Id);

            if (viewModel.UserDetails.Role == RoleManager.Company)
            {
                user.CompanyId = viewModel.UserDetails.User.CompanyId;
            }

            if (previousRoleValue == RoleManager.Company)
            {
                user.CompanyId = null;
            }

            await _workUnit.SaveAsync();

            await _userManager.RemoveFromRoleAsync(user, previousRoleValue);
            await _userManager.AddToRoleAsync(user, viewModel.UserDetails.Role!);
        }

        return RedirectToAction(nameof(Index));
    }
}