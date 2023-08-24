namespace Online_Bookstore_API.Services.OrdersFolder
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllUserOrdersAsync(String UserName);
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(int OrderId);
        Task<Order> CreateOrderAsync(String UserName);

    }
}
