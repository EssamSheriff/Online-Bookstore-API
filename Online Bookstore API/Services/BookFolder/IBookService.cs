using Online_Bookstore_API.Models.DTOs;

namespace Online_Bookstore_API.Services.BookFolder
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooks();
        Task<List<Book>> GetBookByTitle(String title);
        Task<List<Book>> GetBookByAuthor(String Author);
        Task<List<Book>> GetBookByGenre(String Genre);
        Task<Book> CreateNewBook(BookDto bookDto);
        Task<Book>? DeleteBook(int BookId);
        Task<Book>? EditBook(int BookId, BookDto bookDto);

    }
}