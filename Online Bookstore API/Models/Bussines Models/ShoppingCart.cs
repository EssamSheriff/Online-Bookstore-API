namespace Online_Bookstore_API.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public double TotalPrice { get; set; }

        public List<CartItem> cartItems { get; set; } 

        public bool IsOrdered { get; set; }
    }
}
