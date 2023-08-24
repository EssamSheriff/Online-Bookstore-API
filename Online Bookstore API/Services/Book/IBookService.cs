namespace Online_Bookstore_API.Services.Book
{
    public interface IBookService
    {
        Task<Book> GetBook(String model);
     }
}
