using System.Security.Claims;
using BookBazaar.Data.Repo.Interfaces;
using BookBazaar.Models.OrderModels;
using BookBazaar.Models.VM;
using Microsoft.AspNetCore.Mvc;

namespace BookBazaarWeb.Areas.Customer.Controllers;

[Area("Customer")]
public class OrderController : Controller
{
    private readonly IWorkUnit _workUnit;

    public OrderController(IWorkUnit workUnit)
    {
        _workUnit = workUnit;
    }

    public async Task<IActionResult> Index(int? id = null)
    {
        ClaimsIdentity claimsIdentity = (ClaimsIdentity)User.Identity!;
        string loggedInUserId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        IEnumerable<Order> orders = await _workUnit.OrderRepo.RetrieveAllAsync(o => o.UserId == loggedInUserId);
        IList<OrderViewModel> viewModel = new List<OrderViewModel>();

        foreach (var order in orders)
        {
            IEnumerable<OrderInfo> ordersInfo =
                await _workUnit.OrderInfoRepo.RetrieveAllAsync(oi => oi.OrderId == order.Id);
            viewModel.Add(new()
            {
                Order = order,
                OrderInfos = ordersInfo
            });
        }

        return View(viewModel.OrderByDescending(vm => vm.Order.OrderDate));
    }

    public async Task<IActionResult> Details(int orderId)
    {
        Order order = await _workUnit.OrderRepo.GetAsync(o => o.Id == orderId, "User");

        if (order is null)
        {
            return NotFound();
        }

        IEnumerable<OrderInfo> infos =
            await _workUnit.OrderInfoRepo.RetrieveAllAsync(oi => oi.OrderId == orderId, "Book,InventoryItem");

        OrderViewModel viewModel = new()
        {
            Order = order,
            OrderInfos = infos,
        };

        return PartialView("_OrderDetails", viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Update(OrderViewModel viewModel)
    {
        if (viewModel.Order is null)
        {
            return NotFound();
        }

        Order? order = await _workUnit.OrderRepo.GetAsync(o => o.Id == viewModel.Order.Id);

        if (order is null)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> PerformBusinessPayment()
    {
        return RedirectToAction("Index");
    }
}