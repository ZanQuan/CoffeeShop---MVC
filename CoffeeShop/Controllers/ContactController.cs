using CoffeeShop.Data;
using CoffeeShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Controllers
{
    public class ContactController : Controller
    {
        private CoffeeshopDbContext dbContext;

        public ContactController(CoffeeshopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Contact contact)
        {
            contact.SentAt = DateTime.Now;
            dbContext.Contacts.Add(contact);
            dbContext.SaveChanges();
            ViewBag.Success = "Cảm ơn bạn đã liên hệ!";
            return View();
        }
    }
}