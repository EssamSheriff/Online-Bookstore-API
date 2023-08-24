using Microsoft.EntityFrameworkCore;
using Online_Bookstore_API.Services.ShoppingCart;

namespace Online_Bookstore_API.Services.OrdersFolder
{
    public class OrderService : IOrderService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IShoppingCartService _shoppingCartService;

        public OrderService(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IShoppingCartService shoppingCartService)
        {
            _userManager = userManager;
            _context = context;
            _shoppingCartService = shoppingCartService;
        }

        public async Task<Order> CreateOrderAsync(string UserName)
        {
            var UserShoppingCart = await _shoppingCartService.GetShoppingCartItems(UserName)!;
            if (UserShoppingCart == null) return null!;
            Order order = new Order { ShoppingCart = UserShoppingCart, UserId = UserShoppingCart.UserId,CreatedDate=DateTime.Now };
            await _context.Order.AddAsync(order);
            UserShoppingCart.IsOrdered = true;
            _context.ShoppingCart.Update(UserShoppingCart);
            await _context.SaveChangesAsync();
            return order;

        }

        public async Task<List<Order>> GetAllOrdersAsync() => await _context.Order.Include(c => c.ShoppingCart.cartItems).OrderBy(o=> o.Id).ToListAsync();


        public async Task<List<Order>> GetAllUserOrdersAsync(string UserName)
        {
            var User = await _userManager.FindByNameAsync(UserName);
            if (User == null) return null!;
            var orders = await _context.Order.Include(c=>c.ShoppingCart.cartItems).Where(o => o.UserId == User.Id ).OrderBy(o=> o.CreatedDate).ToListAsync();
            return orders;
        }

        public async Task<Order> GetOrderByIdAsync(int OrderId) => await _context.Order.Include(c=> c.ShoppingCart.cartItems).FirstOrDefaultAsync(o=> o.Id == OrderId);
    }
}
