using System.Text.Json.Serialization;

namespace Online_Bookstore_API.Models
{
    public class AuthModel
    {
        public String Message { get; set; }
        public bool IsAuthentticated { get; set; }
        public String UserName { get; set; }
        public string Email { get; set; }
        public List<String> Roles { get; set; }
        public String Token { get; set; }
        public DateTime Expiresion { get; set; }  
        
        [JsonIgnore]
        public String? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }  
        public ShoppingCart? ShoppingCart { get; set; }


    }
}
