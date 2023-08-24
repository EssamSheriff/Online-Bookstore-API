
namespace Online_Bookstore_API.Services.Users
{
    public interface IUserService
    {
        Task<UserInfoDto> GetUserInfoAsync(String UserName);

        Task<UserInfoDto> EditUserInfoAsync(String UserName, UserInfoDto userInfoDto);
        Task<bool> DeleteUserAsync(String UserName);
        Task<String> ChangePasswordAsync(String UserName, string OldPassword,String NewPassword);
    }
}
