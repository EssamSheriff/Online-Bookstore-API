using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Bookstore_API.Models.DTOs;
using Online_Bookstore_API.Services.ShoppingCart;

namespace Online_Bookstore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;

        public CartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpGet("{UserName}")]
        public async Task<IActionResult> GetCartItems(String UserName)
        {
            var ShoppingCart = await _shoppingCartService.GetShoppingCartItems(UserName)!;
            return ShoppingCart == null ? NotFound("User Not Found OR Empty Cart..!") :Ok(ShoppingCart);
        }

        [HttpPut("Update/{Id}")]
        public async Task<IActionResult> UpdateItemQuantity(int id, [FromForm] int Quantity)
        {
            if (Quantity == 0) return BadRequest("Quantity must be > 0 ");
            var item = await _shoppingCartService.UpdateCartItem(id, Quantity)!;
            return item == null ? NotFound() : Ok(item);
        }

        [HttpDelete("Remove/{Id}")]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
           return await _shoppingCartService.RemoveCartItem(id) ? Ok("Deleted") :BadRequest() ;
        }
        
        [HttpPost("Add")]
        public async Task<IActionResult> AddToCart([FromBody] CartDto cartDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var ShoppingCart = await _shoppingCartService.AddToShoppingCart(cartDto)!;
            return ShoppingCart == null ? NotFound("User Or Book Not Found...!") : Ok(ShoppingCart);
        }  
    }
}
