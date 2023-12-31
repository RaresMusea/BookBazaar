﻿using System.Security.Claims;
using BookBazaar.Data.Repo.Interfaces;
using BookBazaar.Models.OrderModels;
using BookBazaar.Models.VM;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

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

        IEnumerable<Order> orders = await _workUnit.OrderRepo.RetrieveAllAsync(o => o.UserId == loggedInUserId, "User");
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

        return RedirectToAction(nameof(Index), new { id = viewModel.Order.Id });
    }

    [HttpPost]
    public async Task<IActionResult> PerformBusinessPayment(int id)
    {
        Order order = await _workUnit.OrderRepo.GetAsync(o => o.Id == id, "User");

        if (order is null)
        {
            return NotFound();
        }

        IEnumerable<OrderInfo> info = await _workUnit.OrderInfoRepo.RetrieveAllAsync(i => i.OrderId == id,
            "Book,InventoryItem");

        const string domain = @"https://localhost:7279";
        var options = new SessionCreateOptions
        {
            SuccessUrl = $"{domain}/Customer/OrderBasket/ConfirmBusinessOrder?orderId={order.Id}",
            CancelUrl = $"{domain}/Customer/Order/Index/${order.Id}",
            LineItems = new List<SessionLineItemOptions>(),
            Mode = "payment",
        };

        int pricesIdx = 0;
        IList<double> unitaryPrices = new List<double>();

        foreach (OrderInfo infoElement in info)
        {
            if (infoElement.Discount == 0.00)
            {
                unitaryPrices.Add(Math.Round(infoElement.Price, 2));
            }
            else
            {
                unitaryPrices.Add(Math.Round(infoElement.Price - infoElement.Discount * infoElement.Price, 2));
            }


            SessionLineItemOptions sessionLineItemOptions = new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)(unitaryPrices[pricesIdx++] * 100),
                    Currency = "eur",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = $"{infoElement.Book.Title} by {infoElement.Book.Author}",
                    }
                },
                Quantity = infoElement.Amount
            };

            options.LineItems.Add(sessionLineItemOptions);
        }

        var service = new SessionService();
        Session session = await service.CreateAsync(options);
        await _workUnit.OrderRepo.UpdateStripeIdAsync(order.Id, session.Id, session.PaymentIntentId);
        await _workUnit.SaveAsync();

        Response.Headers.Add("Location", session.Url);
        return new StatusCodeResult(303);
    }
}