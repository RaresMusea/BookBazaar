using System.Security.Claims;
using BookBazaar.Data.Repo.Interfaces;
using BookBazaar.Misc.Session;
using Microsoft.AspNetCore.Mvc;

namespace BookBazaarWeb.ViewComponents;

public class OrderBasketCounterViewComponent : ViewComponent
{
    private readonly IWorkUnit _workUnit;

    public OrderBasketCounterViewComponent(IWorkUnit workUnit)
    {
        _workUnit = workUnit;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        ClaimsIdentity claimsIdentity = (ClaimsIdentity)User.Identity!;

        var claim = claimsIdentity!.FindFirst(ClaimTypes.NameIdentifier);

        if (claim is not null)
        {
            if (HttpContext.Session.GetInt32(SessionManager.OrderBasketSession) is null)
            {
                int actualAmount = (await _workUnit.OrderBasketRepo.RetrieveAllAsync(b => b.UserId == claim.Value))
                    .Count();
                HttpContext.Session.SetInt32(SessionManager.OrderBasketSession, actualAmount);
            }

            return View(HttpContext.Session.GetInt32(SessionManager.OrderBasketSession));
        }
        else
        {
            HttpContext.Session.Clear();
            return View(0);
        }
    }
}