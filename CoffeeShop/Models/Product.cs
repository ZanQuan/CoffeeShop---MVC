namespace CoffeeShop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Detail { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public bool IsTrendingProduct { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public int? BranchId { get; set; }
        public Branch? Branch { get; set; }
    }
}
