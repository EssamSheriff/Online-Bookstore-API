using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Online_Bookstore_API.Models.DTOs;

namespace Online_Bookstore_API.Services.ShoppingCart
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ShoppingCartService(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<Models.ShoppingCart>? AddToShoppingCart(CartDto cartDto)
        {
            var user = await GetUser(cartDto.Username)!;
            var book = await GetBook(cartDto.BookId)!;

            if (user == null || book == null ) return null!;

            if (book.Copies <= cartDto.Quantity)
                return null!;
            else
            {
                book.Copies -= cartDto.Quantity;
                _context.Update(book);
            }

            var item = new CartItem
            {
                BookId = cartDto.BookId,
                Quantity = cartDto.Quantity == 0 ? 1 : cartDto.Quantity,
                SubTotal = cartDto.Quantity != 0 ? (cartDto.Quantity * book.Price) : book.Price,
            };

            user.shoppingCart = await GetUserShoppingCart(user)!;

            if (user.shoppingCart == null)
            {
                user.shoppingCart = new Models.ShoppingCart();
                user.shoppingCart.cartItems = new List<CartItem> { item };
                user.shoppingCart.TotalPrice = item.SubTotal;
                user.shoppingCart.UserId = user.Id;
            }
            else
            {
                user.shoppingCart.cartItems.Add(item);
                user.shoppingCart.TotalPrice += item.SubTotal;
            }
            await _userManager.UpdateAsync(user);
            await _context.SaveChangesAsync();
            return user.shoppingCart;
        }

        public async Task<Models.ShoppingCart>? GetShoppingCartItems(string UserName)
        {
            var user = await GetUser(UserName)!;
            if (user == null) return null!;
            return await GetUserShoppingCart(user)!;
        }

        public async Task<bool> RemoveCartItem(int ItemId)
        {
            var item = await _context.CartItem.FindAsync(ItemId);
            if (item == null) return false;
            _context.CartItem.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CartItem>? UpdateCartItem(int ItemId,int Quantity)
        {
            var item = await _context.CartItem.FindAsync(ItemId);
            if (item == null) return null ;
            var price = item.SubTotal / item.Quantity;
            item.Quantity = Quantity;
            item.SubTotal = Quantity * price;
            _context.CartItem.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }


        private async Task<ApplicationUser> GetUser(string UserName) =>  await _userManager.FindByNameAsync(UserName);
        private async Task<Book> GetBook(int BookId) => await _context.Books.FindAsync(BookId);

        private async Task<Models.ShoppingCart>? GetUserShoppingCart(ApplicationUser user)
        {
            user.shoppingCart = await _context.ShoppingCart.FirstOrDefaultAsync(cart => cart.UserId == user.Id && cart.IsOrdered == false);
            if (user.shoppingCart == null) return null!;
            user.shoppingCart.cartItems = await _context.CartItem.Where(item => item.ShoppingCartId == user.shoppingCart.Id).ToListAsync();
            user.shoppingCart.TotalPrice = 0;
            foreach (var item in user.shoppingCart.cartItems)
                user.shoppingCart.TotalPrice += item.SubTotal;
            return user.shoppingCart;
        }

    }
}
