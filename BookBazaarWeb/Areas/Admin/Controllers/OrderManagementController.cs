using BookBazaar.Data.Repo.Interfaces;
using BookBazaar.Models.OrderModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BookBazaarWeb.Areas.Admin.Controllers;

[Area("Admin")]
public class OrderManagementController : Controller
{
    private readonly IWorkUnit _workUnit;

    public OrderManagementController(IWorkUnit workUnit)
    {
        _workUnit = workUnit;
    }

    public async Task<IActionResult> Index(string? state = null)
    {
        IEnumerable<Order> orders = await _workUnit.OrderRepo.RetrieveAllAsync(includedProperties: "User");

        if (!state.IsNullOrEmpty())
        {
            orders = FilterOrdersBasedOnState(state, orders);
        }

        return View(orders);
    }

    private IEnumerable<Order> FilterOrdersBasedOnState(string? state, IEnumerable<Order> orders)
    {
        switch (state)
        {
            case "Pending":
                orders = orders.Where(o => o.OrderState == "Pending");
                break;
            case "Approved":
                orders = orders.Where(o => o.OrderState == "Approved");
                break;
            case "Refunded":
                orders = orders.Where(o => o.OrderState == "Refunded");
                break;
            case "Canceled":
                orders = orders.Where(o => o.OrderState == "Canceled");
                break;
            case "Processing":
                orders = orders.Where(o => o.OrderState == "Processing");
                break;
            case "Delivered":
                orders = orders.Where(o => o.OrderState == "Delivered");
                break;
            default:
                return orders;
        }

        return orders;
    }
}