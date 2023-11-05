using System.Security.Claims;
using BookBazaar.Data.Repo.Interfaces;
using BookBazaar.Models.CartModels;
using BookBazaar.Models.VM;
using BookBazaarWeb.Areas.Customer.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookBazaarWeb.Areas.Customer.Controllers;

[Area("Customer")]
[Authorize]
public class OrderBasketController : Controller
{
    private readonly IWorkUnit _workUnit;

    public OrderBasketController(IWorkUnit workUnit)
    {
        _workUnit = workUnit;
    }

    public async Task<IActionResult> Index()
    {
        ClaimsIdentity claimsIdentity = (ClaimsIdentity)User.Identity!;
        string loggedInUserId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        IEnumerable<OrderBasket> orderBasketElements =
            await _workUnit.OrderBasketRepo.RetrieveAllAsync(b => b.UserId == loggedInUserId, "Book,InventoryItem");

        Dictionary<int, double> discounts = OrderBasketControllerUtils.ComputeDiscounts(orderBasketElements);

        DetailedOrderBasketViewModel viewModel = new()
        {
            OrderBasketList = orderBasketElements,
            Total = OrderBasketControllerUtils.GrandTotal,
            BookDiscounts = discounts
        };

        return View(viewModel);
    }
}