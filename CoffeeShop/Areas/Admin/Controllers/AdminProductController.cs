using CoffeeShop.Data;
using CoffeeShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminProductController : Controller
    {
        private readonly CoffeeshopDbContext _db;
        public AdminProductController(CoffeeshopDbContext db) { _db = db; }

        public IActionResult Index()
        {
            return View(_db.Products.ToList());
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var p = _db.Products.Find(id);
            if (p == null) return NotFound();
            return View(p);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            _db.Products.Update(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var p = _db.Products.Find(id);
            if (p != null) { _db.Products.Remove(p); _db.SaveChanges(); }
            return RedirectToAction("Index");
        }
    }
}