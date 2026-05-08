using CoffeeShop.Models.Interfaces;
using CoffeeShop.Data;
namespace CoffeeShop.Models.Services
{
    public class ProductRepository : IProductRepository
    {
        private CoffeeshopDbContext _dbContext;

        public ProductRepository(CoffeeshopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Product> GetAllProducts() => _dbContext.Products;

        public Product? GetProductDetail(int id)
            => _dbContext.Products.FirstOrDefault(p => p.Id == id);

        public IEnumerable<Product> GetTrendingProducts()
            => _dbContext.Products.Where(p => p.IsTrendingProduct);
    }
}
