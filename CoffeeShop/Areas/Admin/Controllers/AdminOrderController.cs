using CoffeeShop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminOrderController : Controller
    {
        private readonly CoffeeshopDbContext _db;
        public AdminOrderController(CoffeeshopDbContext db) { _db = db; }

        public IActionResult Index()
        {
            var orders = _db.Order
                .Include(o => o.OrderDetails)
                .ThenInclude(d => d.Product)
                .OrderByDescending(o => o.OrderPlaced)
                .ToList();
            return View(orders);
        }
    }
}