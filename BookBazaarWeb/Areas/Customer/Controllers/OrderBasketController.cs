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
    private readonly OrderBasketControllerUtils _utils;

    public OrderBasketController(IWorkUnit workUnit, OrderBasketControllerUtils utils)
    {
        _workUnit = workUnit;
        _utils = utils;
    }

    public async Task<IActionResult> Index()
    {
        ClaimsIdentity claimsIdentity = (ClaimsIdentity)User.Identity!;
        string loggedInUserId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        IEnumerable<OrderBasket> orderBasketElements =
            await _workUnit.OrderBasketRepo.RetrieveAllAsync(b => b.UserId == loggedInUserId, "Book,InventoryItem");

        Dictionary<int, double> discounts = _utils.ComputeDiscounts(orderBasketElements);

        DetailedOrderBasketViewModel viewModel = new()
        {
            OrderBasketList = orderBasketElements,
            Total = _utils.GrandTotal,
            BookDiscounts = discounts
        };

        ViewData["Savings"] = _utils.Savings;
        ViewData["DiscountsApplied"] = _utils.DiscountsApplied;
        ViewData["TotalWithoutDiscount"] = _utils.TotalWithoutDiscount;
        ViewData["ProductsAmount"] = _utils.TotalProducts;

        return View(viewModel);
    }

    public async Task<IActionResult> IncreaseQuantity(int orderBasketId)
    {
        if (orderBasketId <= 0)
        {
            return NotFound();
        }

        OrderBasket? orderBasket = await _workUnit.OrderBasketRepo.GetAsync(o => o.Id == orderBasketId);

        if (orderBasket is null)
        {
            return NotFound();
        }

        orderBasket.Items++;

        _workUnit.OrderBasketRepo.Update(orderBasket);
        await _workUnit.SaveAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> DecreaseQuantity(int orderBasketId)
    {
        if (orderBasketId <= 0)
        {
            return NotFound();
        }

        OrderBasket? orderBasket = await _workUnit.OrderBasketRepo.GetAsync(o => o.Id == orderBasketId);

        if (orderBasket is null)
        {
            return NotFound();
        }

        orderBasket.Items--;

        _workUnit.OrderBasketRepo.Update(orderBasket);
        await _workUnit.SaveAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> RemoveOrderBasketItem(int orderBasketId)
    {
        if (orderBasketId <= 0)
        {
            return NotFound();
        }

        OrderBasket? basket = await _workUnit.OrderBasketRepo.GetAsync(b => b.Id == orderBasketId);

        if (basket is null)
        {
            return NotFound();
        }

        _workUnit.OrderBasketRepo.Remove(basket);
        await _workUnit.SaveAsync();

        return RedirectToAction(nameof(Index));
    }

    public IActionResult ShippingDetails()
    {
        return View("ShippingDetails");
    }
}