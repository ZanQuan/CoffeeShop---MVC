namespace CoffeeShop.Models.Interfaces
{
    public interface IOrderRepository
    {
        List<Order> GetOrdersByUser(string userId);
        void PlaceOrder(Order order);
    }
}