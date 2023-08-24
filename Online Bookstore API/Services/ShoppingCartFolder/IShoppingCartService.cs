using Online_Bookstore_API.Models.DTOs;

namespace Online_Bookstore_API.Services.ShoppingCart
{
    public interface IShoppingCartService
    {
        Task<Models.ShoppingCart>? GetShoppingCartItems(String UserName);
        Task<Models.ShoppingCart>? AddToShoppingCart(CartDto cartDto);
        Task<CartItem>? UpdateCartItem(int ItemId,int Quantity);
        Task<bool> RemoveCartItem(int ItemId);
        
    }
}
