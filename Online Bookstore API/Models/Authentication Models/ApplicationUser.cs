using Microsoft.AspNetCore.Identity;

namespace Online_Bookstore_API.Models
{
    public class ApplicationUser :IdentityUser
    {
        [MaxLength(50)]
        public String FirstName { get; set; }
        [MaxLength(50)]
        public String LastName { get; set; } 
        [MaxLength(500)]
        public String? Address { get; set; }

        public List<RefreshToken>? RefreshTokens { get; set; }
        public List<Order>? Orders { get; set; }
        public ShoppingCart? shoppingCart { get; set; }
    }
}
