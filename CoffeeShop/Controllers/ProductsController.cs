using CoffeeShop.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Shop()
        { 
            return View(_productRepository.GetAllProducts()); 
        }

        public IActionResult Detail(int id)
        {
            return View(_productRepository.GetProductDetail(id));
        }
    }
}
