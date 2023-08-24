﻿namespace Online_Bookstore_API.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public double SubTotal { get; set; }
        public int ShoppingCartId { get; set; }
    }
}
