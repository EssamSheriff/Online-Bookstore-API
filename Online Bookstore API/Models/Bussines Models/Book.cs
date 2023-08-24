namespace Online_Bookstore_API.Models
{
    public class Book
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }

        public required string Genre { get; set; }

        public required string Author { get; set; }

        public float Price { get; set; }
        public int Copies { get; set; }

        public bool IsAvailable { get; set; }

    }
}
