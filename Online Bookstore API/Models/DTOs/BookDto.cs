namespace Online_Bookstore_API.Models.DTOs
{
    public class BookDto
    {
        public required string Title { get; set; }
        public required string Description { get; set; }

        public required string Genre { get; set; }

        public required string Author { get; set; }

        public required float Price { get; set; }
        public required int Copies { get; set; }
    }
}
