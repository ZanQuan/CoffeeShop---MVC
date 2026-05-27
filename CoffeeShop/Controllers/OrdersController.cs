using CoffeeShop.Models;
using CoffeeShop.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Controllers
{
    public class OrdersController : Controller
    {
        private IOrderRepository orderRepository;
        private IShoppingCartRepository shoppingCartRepository;

        public OrdersController(IOrderRepository orderRepository,
                                IShoppingCartRepository shoppingCartRepository)
        {
            this.orderRepository = orderRepository;
            this.shoppingCartRepository = shoppingCartRepository;
        }

        // GET: Hiển thị form checkout
        public IActionResult Checkout()
        {
            return View();
        }

        // POST: Xử lý đơn hàng khi submit form
        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            orderRepository.PlaceOrder(order);
            shoppingCartRepository.ClearCart();
            HttpContext.Session.SetInt32("CartCount", 0); // Reset cart counter về 0
            return RedirectToAction("CheckoutComplete");
        }

        // Trang xác nhận đặt hàng thành công
        public IActionResult CheckoutComplete()
        {
            return View();
        }
    }
}