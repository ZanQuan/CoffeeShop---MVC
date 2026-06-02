using CoffeeShop.Data;
using CoffeeShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
            // Include Category và Branch khi query
            var products = _db.Products
                .Include(p => p.Category)
                .Include(p => p.Branch)
                .ToList();
            return View(products);
        }

        public IActionResult Create()
        {
            LoadDropdowns();
            return View();
        }

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
            LoadDropdowns();
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

        // Helper: truyền danh sách Category + Branch xuống View
        private void LoadDropdowns()
        {
            ViewBag.Categories = new SelectList(_db.Categories.ToList(), "Id", "Name");
            ViewBag.Branches = new SelectList(_db.Branches.ToList(), "Id", "Name");
        }
    }
}