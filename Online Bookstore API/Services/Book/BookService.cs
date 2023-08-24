using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Azure;
using Microsoft.EntityFrameworkCore;
using Online_Bookstore_API.Services.Book;

namespace Online_Bookstore_API.Services.Authentication
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _DbContext;

        public BookService(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
        }

    }
}
