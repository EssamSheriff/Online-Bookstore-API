namespace Online_Bookstore_API.Models
{
    public class Order
    {
        public int Id { get; set; }
        public required String UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DeliveryDate => CreatedDate.AddDays(10);
        public required ShoppingCart ShoppingCart { get; set; }

    }
}
