using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_Bookstore_API.Models.DTOs;
using Online_Bookstore_API.Services.Authentication;

namespace Online_Bookstore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel registerModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var User = await _authService.RegisterAsync(registerModel);
            if (!User.IsAuthentticated)
                return BadRequest(User.Message);
            SetRefreshTokenInCookie(User.RefreshToken, User.RefreshTokenExpiration);

            return Ok(User);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var User = await _authService.LoginAsync(loginModel);
            if (!User.IsAuthentticated)
                return BadRequest(User.Message);

            if (!string.IsNullOrEmpty(User.RefreshToken))
                SetRefreshTokenInCookie(User.RefreshToken, User.RefreshTokenExpiration);
            return Ok(User);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("AddToRole")]
        public async Task<IActionResult> AddToRoleAsync([FromBody] RoleModel roleModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var Message = await _authService.AddUserToRole(roleModel);
            if (Message == "Done!")
                return Ok(Message);

            return BadRequest(Message);
        }

        [HttpGet("RefreshToken")]
        public async Task<IActionResult> GetRefreshTokenAsync()
        {
            // get old RefreshToken from Cookies
            var Result = await _authService.RefreshTokenAsync(Request.Cookies["RefreshToken"]);

            if (!Result.IsAuthentticated) return BadRequest(Result);
            SetRefreshTokenInCookie(Result.RefreshToken, Result.RefreshTokenExpiration);

            return Ok(Result);
        }

        [HttpPost("RevokeToken")]
        public async Task<IActionResult> RevokeTokenAsync([FromBody] RevokeModelDto revokeModelDto)
        {
            // get old RefreshToken from Cookies
            var Token = revokeModelDto.Token ?? Request.Cookies["RefreshToken"];
            if (String.IsNullOrEmpty(Token)) return BadRequest("Token is Required..!");

            return await _authService.RevokeTokenAsync(Token) ? Ok("Done") : BadRequest("Token is Invalid..!");
        }

        private void SetRefreshTokenInCookie(string refreshToken, DateTime expire)
        {
            var Cookieoptions = new CookieOptions
            {
                Expires = expire.ToLocalTime(),
                HttpOnly = true,
            };
            Response.Cookies.Append("RefreshToken", refreshToken,Cookieoptions);
        }
    }
}
