namespace Online_Bookstore_API.Models.DTOs
{
    public class CartDto
    {
        public required string Username { get; set; }
        public required int BookId { get; set; }
        public int Quantity { get; set; }
    }
}
