using CoffeeShop.Data;
using CoffeeShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminCategoryController : Controller
    {
        private readonly CoffeeshopDbContext _db;

        public AdminCategoryController(CoffeeshopDbContext db)
        {
            _db = db;
        }

        // Danh sách Category
        public IActionResult Index()
        {
            var list = _db.Categories.ToList();
            return View(list);
        }

        // Form thêm mới
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Form sửa
        public IActionResult Edit(int id)
        {
            var item = _db.Categories.Find(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            _db.Categories.Update(category);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Xóa
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var item = _db.Categories.Find(id);
            if (item != null)
            {
                _db.Categories.Remove(item);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}