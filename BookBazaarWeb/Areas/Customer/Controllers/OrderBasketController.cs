using System.Security.Claims;
using BookBazaar.Data.Repo.Interfaces;
using BookBazaar.Misc;
using BookBazaar.Models.CartModels;
using BookBazaar.Models.OrderModels;
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
        await _workUnit.SaveAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Checkout()
    {
        string loggedInUserId = GetLoggedUserId();

        DetailedOrderBasketViewModel viewModel = await BuildViewModel(loggedInUserId);

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

        await _workUnit.OrderRepo.CreateAsync(viewModel.Order);
        await _workUnit.SaveAsync();

        foreach (var orderBasketItem in viewModel.OrderBasketList)
        {
            OrderInfo info = new()
            {
                Amount = orderBasketItem.Items,
                BookId = orderBasketItem.BookId,
                OrderId = viewModel.Order.Id,
                InventoryItemId = orderBasketItem.InventoryItemId,
                Discount = viewModel.BookDiscounts[orderBasketItem.BookId],
                Price = viewModel.BookDiscounts[orderBasketItem.BookId] == 0.00
                    ? (orderBasketItem.Book.Price * orderBasketItem.Items)
                    : (orderBasketItem.Book.Price * orderBasketItem.Items -
                       (viewModel.BookDiscounts[orderBasketItem.BookId] * (orderBasketItem.Book.Price *
                                                                           orderBasketItem.Items))),
            };

            await _workUnit.OrderInfoRepo.CreateAsync(info);
            await _workUnit.SaveAsync();
        }

        if (isRegularCustomer)
        {
            //TODO: PROCESS PAYMENT
        }

        TempData["Name"] = viewModel.Order.Name;
        TempData["OrderId"] = viewModel.Order.Id;
        return RedirectToAction(nameof(ConfirmOrder));
    }

    public IActionResult ConfirmOrder()
    {
        OrderConfirmationViewModel viewModel = new()
        {
            Name = (string)TempData["Name"]!,
            OrderId = (int)TempData["OrderId"]!
        };

        TempData.Keep("Name");
        TempData.Keep("OrderId");

        return View(viewModel);
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