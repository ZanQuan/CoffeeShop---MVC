using CoffeeShop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminContactController : Controller
    {
        private readonly CoffeeshopDbContext _db;
        public AdminContactController(CoffeeshopDbContext db) { _db = db; }

        public IActionResult Index()
        {
            var contacts = _db.Contacts.OrderByDescending(c => c.SentAt).ToList();
            return View(contacts);
        }
    }
}