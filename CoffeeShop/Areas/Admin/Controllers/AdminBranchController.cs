using CoffeeShop.Data;
using CoffeeShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminBranchController : Controller
    {
        private readonly CoffeeshopDbContext _db;

        public AdminBranchController(CoffeeshopDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var list = _db.Branches.ToList();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Branch branch)
        {
            _db.Branches.Add(branch);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var item = _db.Branches.Find(id);
            if (item == null) return NotFound();
            return View(item);
        }

        [HttpPost]
        public IActionResult Edit(Branch branch)
        {
            _db.Branches.Update(branch);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var item = _db.Branches.Find(id);
            if (item != null)
            {
                _db.Branches.Remove(item);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}