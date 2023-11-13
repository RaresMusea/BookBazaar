using BookBazaar.Data.Repo.Interfaces;
using BookBazaar.Misc.Orders_Payments;
using BookBazaar.Misc.Roles;
using BookBazaar.Models.InventoryModels;
using BookBazaar.Models.OrderModels;
using BookBazaar.Models.VM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Stripe;

namespace BookBazaarWeb.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
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

    public async Task<IActionResult> Details(int orderId)
    {
        OrderViewModel? viewModel = await BuildOrderViewModel(orderId);

        if (viewModel is null)
        {
            return NotFound();
        }

        return View(viewModel);
    }

    [Authorize(Roles = $"{RoleManager.Administrator},{RoleManager.Internal}")]
    [HttpPost]
    public async Task<IActionResult> UpdateOrder(OrderViewModel viewModel)
    {
        Order order = await _workUnit.OrderRepo.GetAsync(o => o.Id == viewModel.Order.Id);
        if (viewModel is null || order is null)
        {
            return NotFound();
        }

        order.OrderState = viewModel.Order.OrderState;
        order.Address = viewModel.Order.Address;
        order.Name = viewModel.Order.Name;

        if (!string.IsNullOrEmpty(viewModel.Order.Awb))
        {
            order.Awb = viewModel.Order.Awb;
        }

        if (!string.IsNullOrEmpty(viewModel.Order.ShippingProvider))
        {
            order.ShippingProvider = viewModel.Order.ShippingProvider;
        }

        order.City = viewModel.Order.City;
        order.Country = viewModel.Order.Country;
        order.DeliveryDate = viewModel.Order.DeliveryDate;
        order.GrandTotal = viewModel.Order.GrandTotal;
        order.OrderDate = viewModel.Order.OrderDate;
        order.OrderState = viewModel.Order.OrderState;
        order.PaymentDate = viewModel.Order.PaymentDate;
        order.PhoneNumber = viewModel.Order.PhoneNumber;
        order.TransactionId = viewModel.Order.TransactionId;
        order.TransactionState = viewModel.Order.TransactionState;
        order.PaymentDueDate = viewModel.Order.PaymentDueDate;

        _workUnit.OrderRepo.Update(order);
        await _workUnit.SaveAsync();
        TempData["SuccessfulOperation"] = "Order details were updated successfully!";

        return RedirectToAction(nameof(Details), new { orderId = viewModel.Order.Id });
    }

    [Authorize(Roles = $"{RoleManager.Administrator},{RoleManager.Internal}")]
    [HttpPost]
    public async Task<IActionResult> ProcessOrder(int id)
    {
        if (id <= 0)
        {
            return NotFound();
        }

        IEnumerable<OrderInfo> orderInfo =
            await _workUnit.OrderInfoRepo.RetrieveAllAsync(i => i.OrderId == id, "InventoryItem");

        foreach (var orderDetail in orderInfo)
        {
            InventoryItem orderInventoryItem = orderDetail.InventoryItem;

            if (orderInventoryItem.QuantityInStock - orderDetail.Amount >= 0)
            {
                orderInventoryItem.QuantityInStock -= orderDetail.Amount;
            }
            else
            {
                return NoContent();
            }

            _workUnit.InventoryRepo.Update(orderInventoryItem);
            await _workUnit.SaveAsync();
        }

        await _workUnit.OrderRepo.UpdateOrderStateAsync(id, OrderStatus.Processing);
        await _workUnit.SaveAsync();
        TempData["SuccessfulOperation"] = "Order state set to Processing";

        return RedirectToAction(nameof(Details), new { orderId = id });
    }

    [Authorize(Roles = $"{RoleManager.Administrator},{RoleManager.Internal}")]
    [HttpPost]
    public async Task<IActionResult> Deliver(int id)
    {
        if (id <= 0)
        {
            return NotFound();
        }

        Order order = await _workUnit.OrderRepo.GetAsync(o => o.Id == id);
        order.DeliveryDate = DateTime.Now;

        if (order.TransactionState == PaymentStatus.BusinessDelayed)
        {
            order.PaymentDueDate = DateTime.Now.AddDays(30);
        }

        IEnumerable<OrderInfo> orderInfos =
            await _workUnit.OrderInfoRepo.RetrieveAllAsync(i => i.OrderId == id, "InventoryItem");

        if (order.TransactionState != PaymentStatus.BusinessDelayed)
        {
            foreach (var info in orderInfos)
            {
                InventoryItem infoInventoryItem = info.InventoryItem;
                infoInventoryItem.QuantitySold += info.Amount;

                _workUnit.InventoryRepo.Update(infoInventoryItem);
                await _workUnit.SaveAsync();
            }
        }

        order.OrderState = OrderStatus.Delivered;

        _workUnit.OrderRepo.Update(order);
        await _workUnit.SaveAsync();
        TempData["SuccessfulOperation"] = "The order was delivered.";

        return RedirectToAction(nameof(Details), new { orderId = id });
    }

    [Authorize(Roles = $"{RoleManager.Administrator},{RoleManager.Internal}")]
    [HttpPost]
    public async Task<IActionResult> Revoke(int id)
    {
        if (id <= 0)
        {
            return NotFound();
        }

        Order order = await _workUnit.OrderRepo.GetAsync(o => o.Id == id);

        if (order.TransactionState == PaymentStatus.Approved)
        {
            RefundCreateOptions options = new RefundCreateOptions
            {
                Reason = RefundReasons.RequestedByCustomer,
                PaymentIntent = order.TransactionId
            };

            RefundService service = new RefundService();
            await service.CreateAsync(options);
            await _workUnit.OrderRepo.UpdateOrderStateAsync(order.Id, OrderStatus.Canceled, PaymentStatus.Refunded);
        }
        else
        {
            await _workUnit.OrderRepo.UpdateOrderStateAsync(order.Id, OrderStatus.Canceled, OrderStatus.Canceled);
        }

        await _workUnit.SaveAsync();
        TempData["SuccessfulOperation"] = "Order was canceled and then revoked!";

        return RedirectToAction(nameof(Details), new { orderId = order.Id });
    }

    public IActionResult Error()
    {
        return PartialView("_InventoryError");
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

    private async Task<OrderViewModel?> BuildOrderViewModel(int orderId)
    {
        Order order = await _workUnit.OrderRepo.GetAsync(o => o.Id == orderId, "User");

        if (order is null)
        {
            return null;
        }

        IEnumerable<OrderInfo> orderInfos =
            await _workUnit.OrderInfoRepo.RetrieveAllAsync(oi => oi.OrderId == orderId, "Book,InventoryItem");

        OrderViewModel viewModel = new()
        {
            Order = order,
            OrderInfos = orderInfos
        };

        return viewModel;
    }
}