using Humanizer.Localisation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Bookstore_API.Models.DTOs;
using Online_Bookstore_API.Services.BookFolder;

namespace Online_Bookstore_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService) =>  _bookService = bookService; 

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookService.GetAllBooks();
            return books.Count == 0 ? NotFound("NO Books Found") : Ok(books);

        }

        [HttpGet("Genre/{genre}")]
        public async Task<IActionResult> GetBooksByGenre(string genre)
        {
            var books = await _bookService.GetBookByGenre(genre);
            return books.Count == 0 ? NotFound("NO Books Found") : Ok(books);
        }

        [HttpGet("Title/{title}")]
        public async Task<IActionResult> GetBooksByTitle(string title)
        {
            var books = await _bookService.GetBookByTitle(title);
            return books.Count == 0 ? NotFound("NO Books Found") : Ok(books);
        }

        [HttpGet("Author/{author}")]
        public async Task<IActionResult> GetBooksByAuthor(string Author)
        {
            var books = await _bookService.GetBookByAuthor(Author);
            return books.Count == 0 ? NotFound("NO Books Found") : Ok(books);
        }

        [Authorize(Roles ="Admin")]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateBook([FromBody] BookDto bookDto)
        {
            if(!ModelState.IsValid) return BadRequest();
            Book book = await _bookService.CreateNewBook(bookDto);
            return Ok(book);
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("Edit/{Id}")]
        public async Task<IActionResult> EditBook(int Id, [FromBody] BookDto bookDto)
        {
            if (Id <= 0) return BadRequest("Invalid Id...!");
            var Book = await _bookService.EditBook(Id,bookDto)!;
            return Book == null ? NotFound() : Ok(Book);
        }

        [Authorize(Roles ="Admin")]
        [HttpDelete("Delete/{Id}")]
        public async Task<IActionResult> DeleteBook(int Id)
        {
            if(Id <= 0) return BadRequest("Invalid Id...!");
            var Book = await _bookService.DeleteBook(Id)!;
            return Book == null ? NotFound(): Ok(Book);
        }

    }
}
