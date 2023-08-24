using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Online_Bookstore_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("Info/{UserName}")]
        public async Task<IActionResult> GetUserInfo(String UserName) { 
            if (string.IsNullOrEmpty(UserName)) return BadRequest();
            var UserInfo = await _userService.GetUserInfoAsync(UserName);
            return UserInfo==null ? NotFound("No User Found") : Ok(UserInfo);
        }

        [HttpPost("EditInfo/{UserName}")]
        public async Task<IActionResult> ChangePassword(String UserName,[FromBody] UserInfoDto userInfoDto)
        {
            if (string.IsNullOrEmpty(UserName) || !ModelState.IsValid) return BadRequest(ModelState);
            var UserInfo = await _userService.EditUserInfoAsync(UserName,userInfoDto);
            return UserInfo == null ? NotFound("No User Found") : Ok(UserInfo);
        }

        [HttpPost("ChangePassword/{UserName}")]
        public async Task<IActionResult> EditUserInfo(String UserName, [FromForm] String OldPassword, [FromForm] String NewPassword)
        {
            if (string.IsNullOrEmpty(UserName) || !ModelState.IsValid) return BadRequest(ModelState);
            var Result = await _userService.ChangePasswordAsync(UserName,OldPassword,NewPassword); 

            return Result == "Done" ?  Ok("Done") : NotFound(Result)  ;
        }

        [Authorize(Roles ="Admin")]
        [HttpDelete("Delete/{UserName}")]
        public async Task<IActionResult> DelteUserInfo(String UserName)
        {
            if (string.IsNullOrEmpty(UserName)) return BadRequest();
            //var UserInfo = await _userService.DeleteUserAsync(UserName);
            return await _userService.DeleteUserAsync(UserName) ? Ok("Done") : NotFound("No User Found");
        }

    }
}
