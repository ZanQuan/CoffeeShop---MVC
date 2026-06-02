using CoffeeShop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly CoffeeshopDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public DashboardController(CoffeeshopDbContext db, UserManager<IdentityUser> u)
        {
            _db = db;
            _userManager = u;
        }

        public IActionResult Index()
        {
            ViewBag.TotalProducts = _db.Products.Count();
            ViewBag.TotalOrders = _db.Order.Count();
            ViewBag.TotalUsers = _userManager.Users.Count();
            ViewBag.TotalContacts = _db.Contacts.Count();
            return View();
        }
    }
}