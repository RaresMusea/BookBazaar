using System.Security.Claims;
using BookBazaar.Data.Repo.Interfaces;
using BookBazaar.Misc.Orders_Payments;
using BookBazaar.Misc.Session;
using BookBazaar.Models.CartModels;
using BookBazaar.Models.InventoryModels;
using BookBazaar.Models.OrderModels;
using BookBazaar.Models.VM;
using BookBazaarWeb.Areas.Customer.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

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
        string loggedInUserId = GetLoggedUserId();

        DetailedOrderBasketViewModel viewModel = await BuildViewModel(loggedInUserId);

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
        int itemsAfterRemoval =
            (await _workUnit.OrderBasketRepo.RetrieveAllAsync(b => b.UserId == basket.UserId)).Count();
        HttpContext.Session.SetInt32(SessionManager.OrderBasketSession, itemsAfterRemoval - 1);
        await _workUnit.SaveAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Checkout()
    {
        string loggedInUserId = GetLoggedUserId();

        DetailedOrderBasketViewModel viewModel = await BuildViewModel(loggedInUserId);

        foreach (var orderBasketItem in viewModel.OrderBasketList)
        {
            if (orderBasketItem.InventoryItem.QuantityInStock == 0)
            {
                return NoContent();
            }
        }

        return View(viewModel);
    }

    [HttpPost]
    [ActionName("Checkout")]
    public async Task<IActionResult> CheckoutPost()
    {
        string loggedInUser = GetLoggedUserId();
        DetailedOrderBasketViewModel viewModel = await BuildViewModel(loggedInUser);

        viewModel.Order.OrderDate = DateTime.Now;
        viewModel.Order.UserId = viewModel.Order.User.Id;

        bool isRegularCustomer = viewModel.Order.User.CompanyId.GetValueOrDefault() == 0;

        //Regular user
        if (isRegularCustomer)
        {
            viewModel.Order.OrderState = OrderStatus.Pending;
            viewModel.Order.TransactionState = PaymentStatus.Pending;
        }
        else
        {
            //Company
            viewModel.Order.OrderState = OrderStatus.Approved;
            viewModel.Order.TransactionState = PaymentStatus.BusinessDelayed;
        }

        _workUnit.OrderRepo.Update(viewModel.Order);
        await _workUnit.OrderRepo.CreateAsync(viewModel.Order);
        await _workUnit.SaveAsync();

        IList<double> unitaryPrices = new List<double>();

        foreach (var orderBasketItem in viewModel.OrderBasketList)
        {
            OrderInfo info = new()
            {
                Amount = orderBasketItem.Items,
                BookId = orderBasketItem.BookId,
                OrderId = viewModel.Order.Id,
                InventoryItemId = orderBasketItem.InventoryItemId,
                Discount = viewModel.BookDiscounts[orderBasketItem.BookId],
                Price = orderBasketItem.Book.Price
            };

            if (viewModel.BookDiscounts[orderBasketItem.BookId] == 0.00)
            {
                unitaryPrices.Add(Math.Round(orderBasketItem.Book.Price, 2));
            }
            else
            {
                unitaryPrices.Add(Math.Round(orderBasketItem.Book.Price -
                                             viewModel.BookDiscounts[orderBasketItem.BookId] *
                                             orderBasketItem.Book.Price));
            }

            await _workUnit.OrderInfoRepo.CreateAsync(info);
            await _workUnit.SaveAsync();
        }

        if (isRegularCustomer)
        {
            const string domain = @"https://localhost:7279";
            var options = new SessionCreateOptions
            {
                SuccessUrl = $"{domain}/Customer/OrderBasket/ConfirmOrder?orderId={viewModel.Order.Id}",
                CancelUrl = $"{domain}/Customer/OrderBasket",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
            };

            int pricesIdx = 0;
            foreach (OrderBasket orderBasketItem in viewModel.OrderBasketList)
            {
                SessionLineItemOptions sessionLineItemOptions = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(unitaryPrices[pricesIdx++] * 100),
                        Currency = "eur",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = $"{orderBasketItem.Book.Title} by {orderBasketItem.Book.Author}",
                        }
                    },
                    Quantity = orderBasketItem.Items
                };

                options.LineItems.Add(sessionLineItemOptions);
            }

            var service = new SessionService();
            Session session = await service.CreateAsync(options);
            await _workUnit.OrderRepo.UpdateStripeIdAsync(viewModel.Order.Id, session.Id, session.PaymentIntentId);
            await _workUnit.SaveAsync();

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        return RedirectToAction(nameof(ConfirmOrder), new { orderId = viewModel.Order.Id });
    }

    public async Task<IActionResult> ConfirmOrder(int orderId)
    {
        Order order = await _workUnit.OrderRepo.GetAsync(o => o.Id == orderId, "User");

        if (order.TransactionState != PaymentStatus.BusinessDelayed)
        {
            var service = new SessionService();
            Session session = await service.GetAsync(order.SessionId);

            if (session.PaymentStatus.ToLower() == "paid")
            {
                await _workUnit.OrderRepo.UpdateStripeIdAsync(order.Id, session.Id, session.PaymentIntentId);
                await _workUnit.OrderRepo.UpdateOrderStateAsync(order.Id, OrderStatus.Approved, PaymentStatus.Approved);
                await _workUnit.SaveAsync();
            }
        }

        IEnumerable<OrderBasket> orderBasketItems =
            await _workUnit.OrderBasketRepo.RetrieveAllAsync(b => b.UserId == order.UserId);

        _workUnit.OrderBasketRepo.RemoveRange(orderBasketItems);
        await _workUnit.SaveAsync();

        OrderConfirmationViewModel viewModel = new()
        {
            OrderId = orderId,
            Name = order.Name
        };

        return View(viewModel);
    }

    public async Task<IActionResult> ConfirmBusinessOrder(int orderId)
    {
        Order order = await _workUnit.OrderRepo.GetAsync(o => o.Id == orderId, "User");

        if (order.TransactionState == PaymentStatus.BusinessDelayed)
        {
            var service = new SessionService();
            Session session = await service.GetAsync(order.SessionId);

            if (session.PaymentStatus.ToLower() == "paid")
            {
                await _workUnit.OrderRepo.UpdateStripeIdAsync(order.Id, session.Id, session.PaymentIntentId);
                await _workUnit.OrderRepo.UpdateOrderStateAsync(order.Id, order.OrderState!, PaymentStatus.Approved);
                await _workUnit.SaveAsync();

                IEnumerable<OrderInfo> orderInfos =
                    await _workUnit.OrderInfoRepo.RetrieveAllAsync(i => i.OrderId == orderId, "InventoryItem");

                foreach (var info in orderInfos)
                {
                    InventoryItem infoInventoryItem = info.InventoryItem;
                    infoInventoryItem.QuantitySold += info.Amount;
                    _workUnit.InventoryRepo.Update(infoInventoryItem);
                    await _workUnit.SaveAsync();
                }
            }
        }

        OrderConfirmationViewModel viewModel = new()
        {
            OrderId = orderId,
            Name = order.Name
        };

        return View(viewModel);
    }

    public IActionResult Error()
    {
        return PartialView("_OrderBasketStockError");
    }

    private string GetLoggedUserId()
    {
        ClaimsIdentity claimsIdentity = (ClaimsIdentity)User.Identity!;
        string loggedInUserId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        return loggedInUserId;
    }

    private async Task<DetailedOrderBasketViewModel> BuildViewModel(string loggedUserId)
    {
        IEnumerable<OrderBasket> orderBasketElements =
            await _workUnit.OrderBasketRepo.RetrieveAllAsync(b => b.UserId == loggedUserId, "Book,InventoryItem");

        Dictionary<int, double> discounts = _utils.ComputeDiscounts(orderBasketElements);

        DetailedOrderBasketViewModel viewModel = new()
        {
            OrderBasketList = orderBasketElements,
            Order = new()
            {
                GrandTotal = _utils.GrandTotal,
            },
            BookDiscounts = discounts
        };

        var loggedUser = await _workUnit.UserRepo.GetAsync(u => u.Id == loggedUserId);
        viewModel.Order.User = loggedUser;

        viewModel.Order.Name = viewModel.Order.User.Name;
        viewModel.Order.PhoneNumber = viewModel.Order.User.PhoneNumber!;
        viewModel.Order.Address = viewModel.Order.User.Address;
        viewModel.Order.City = viewModel.Order.User.City;
        viewModel.Order.Country = viewModel.Order.User.Country;

        return viewModel;
    }
}