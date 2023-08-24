using AutoMapper;
using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore;
using Online_Bookstore_API.Models.DTOs;

namespace Online_Bookstore_API.Services.BookFolder
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _Mapper;
        private readonly ILogger<BookService> _logger;

        public BookService(ApplicationDbContext dbContext, IMapper mapper, ILogger<BookService> logger)
        {
            _context = dbContext;
            _Mapper = mapper;
            _logger = logger;
        }

        public async Task<List<Book>> GetAllBooks() => await _context.Books.OrderBy(t => t.Title).ToListAsync();

        public async Task<List<Book>> GetBookByTitle(string title) => await _context.Books.Where(t => t.Title == title).OrderBy(b => b.Title).ToListAsync();
        public async Task<List<Book>> GetBookByAuthor(string author) => await _context.Books.Where(t => t.Author == author).OrderBy(b => b.Title).ToListAsync();
        public async Task<List<Book>> GetBookByGenre(string genre) => await _context.Books.Where(t => t.Genre == genre).OrderBy(b => b.Title).ToListAsync();

        public async Task<Book> CreateNewBook(BookDto bookDto)
        {
            Book book = _Mapper.Map<Book>(bookDto);
            await _context.Books.AddAsync(book);
            _context.SaveChanges();
            return book;
        }

        public async Task<Book>? DeleteBook(int BookId)
        {
            var book = await _context.Books.FindAsync(BookId);
            if (book == null) return null!;
            _context.Books.Remove(book);
            _context.SaveChanges();
            return book;
        }

        public async Task<Book>? EditBook(int BookId, BookDto bookDto)
        {
            var book = await _context.Books.FindAsync(BookId);
            if (book == null) return null!;

            book.Description = bookDto.Description;
            book.Author = bookDto.Author;
            book.Copies = bookDto.Copies;
            book.Price = bookDto.Price;
            book.Price = bookDto.Price;
            book.Title = bookDto.Title;
            
            _context.Update(book);
            _context.SaveChanges();
            return book;
        }
    }
}
