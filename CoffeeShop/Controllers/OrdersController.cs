using CoffeeShop.Models;
using CoffeeShop.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;   
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace CoffeeShop.Controllers
{
    [Authorize]   
    public class OrdersController : Controller
    {
        private IOrderRepository orderRepository;
        private IShoppingCartRepository shoppingCartRepository;
        private readonly UserManager<IdentityUser> _userManager;
        public OrdersController(IOrderRepository orderRepository,
                        IShoppingCartRepository shoppingCartRepository,
                        UserManager<IdentityUser> userManager)
        {
            this.orderRepository = orderRepository;
            this.shoppingCartRepository = shoppingCartRepository;
            this._userManager = userManager;
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            order.UserId = _userManager.GetUserId(User);
            orderRepository.PlaceOrder(order);
            shoppingCartRepository.ClearCart();
            HttpContext.Session.SetInt32("CartCount", 0);
            return RedirectToAction("CheckoutComplete");
        }

        public IActionResult CheckoutComplete()
        {
            return View();
        }
        public IActionResult ListOrders()
        {
            var userId = _userManager.GetUserId(User);
            var orders = orderRepository.GetOrdersByUser(userId);
            return View(orders);
        }
    }
}