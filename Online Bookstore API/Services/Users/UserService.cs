using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Online_Bookstore_API.Services.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _Mapper;

        public UserService(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _Mapper = mapper;
        }

        public async Task<String> ChangePasswordAsync(string UserName, string OldPassword, string NewPassword)
        {
            var User = await _userManager.FindByNameAsync(UserName);
            if (User == null) return String.Empty;
            var Result =  await _userManager.ChangePasswordAsync(User, OldPassword, NewPassword);
            if (!Result.Succeeded)
            {
                StringBuilder errors = new StringBuilder();
                foreach (var error in Result.Errors)
                    errors.Append($"{error.Description} \n");
                return errors.ToString();
            }
            return "Done";
        }

        public async Task<bool> DeleteUserAsync(string UserName)
        {
            var User = await _userManager.FindByNameAsync(UserName);
            if (User == null) return false;
            await _userManager.DeleteAsync(User);
            return true;
        }

        public async Task<UserInfoDto> EditUserInfoAsync(string UserName, UserInfoDto userInfoDto)
        {
            var User = await _userManager.FindByNameAsync(UserName);
            if (User == null) return null!;
            
            User.Address = userInfoDto.Address;
            User.FirstName = userInfoDto.FirstName;
            User.LastName = userInfoDto.LastName;
            User.PhoneNumber = userInfoDto.PhoneNumber;

            await _userManager.UpdateAsync(User);
            return userInfoDto;
        }

        public async Task<UserInfoDto> GetUserInfoAsync(string UserName)
        {
            var User = await _userManager.FindByNameAsync(UserName);
            if (User == null) return null!;
            return _Mapper.Map<UserInfoDto>(User);
        }
    }
}
