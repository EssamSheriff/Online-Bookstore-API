using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Azure;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Online_Bookstore_API.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jWT;
        private readonly IMapper _Mapper;

        public AuthService(UserManager<ApplicationUser> userManager, IOptions<JWT> jWT, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _jWT = jWT.Value;
            _roleManager = roleManager;
            _Mapper = mapper;
        }

        public async Task<string> AddUserToRole(RoleModel model)
        {
            var User = await _userManager.FindByIdAsync(model.UserId);
            if (User == null || !await _roleManager.RoleExistsAsync(model.Role)) return "Invalid User Id Or Role..!!";
            if (await _userManager.IsInRoleAsync(User, model.Role)) return "User already assigned in this role";

            var Result = await _userManager.AddToRoleAsync(User, model.Role);
            return Result.Succeeded ? "Done!" : "Something went wrong";
        }

        public async Task<AuthModel> LoginAsync(LoginModel model)
        {
            var User = await _userManager.FindByEmailAsync(model.Email);
            if (User is null || !await _userManager.CheckPasswordAsync(User, model.Password)) return new AuthModel { Message = "Email or Password is Incorrect...!!" };

            var JwtToken = await CreateJwtToken(User);
            RefreshToken ActiveRefreshToken;

            if (User.RefreshTokens.Any(t => t.IsActive))
                ActiveRefreshToken = User.RefreshTokens.FirstOrDefault(t => t.IsActive);
            else
            {
                ActiveRefreshToken = GetRefreshToken();
                User.RefreshTokens.Add(ActiveRefreshToken);
                await _userManager.UpdateAsync(User);
            }

            return new AuthModel
            {
                IsAuthentticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(JwtToken),
                Email = User.Email,
                UserName = User.UserName,
                Expiresion = JwtToken.ValidTo,
                Message = "Successfully",
                Roles = _userManager.GetRolesAsync(User).Result.ToList(),
                RefreshToken = ActiveRefreshToken.Token,
                RefreshTokenExpiration = ActiveRefreshToken.ExpiresOn,
                ShoppingCart = User.shoppingCart,
            };

        }

        public async Task<AuthModel> RefreshTokenAsync(string token)
        {
            var User = await _userManager.Users.SingleOrDefaultAsync(user => user.RefreshTokens.Any(t => t.Token == token));
            if (User == null) return new AuthModel { IsAuthentticated = false, Message = "Invalid Token" };

            var RefreshToken = User.RefreshTokens.Single(t => t.Token == token);
            if (!RefreshToken.IsActive) return new AuthModel { IsAuthentticated = false, Message = "Inactive Token" };

            // Revoke for old RefreshToken and Create new RefreshToken then add it to list and update database
            RefreshToken.RevokeOn = DateTime.UtcNow;
            var NewRefreshToken = GetRefreshToken();
            User.RefreshTokens.Add(NewRefreshToken);
            await _userManager.UpdateAsync(User);

            // Create New JWT Token 
            var JwtToken = await CreateJwtToken(User);


            var UserRoles = await _userManager.GetRolesAsync(User);

            return new AuthModel
            {
                Email = User.Email,
                UserName = User.UserName,
                Roles = UserRoles.ToList(),
                RefreshToken = NewRefreshToken.Token,
                RefreshTokenExpiration = NewRefreshToken.ExpiresOn,
                Token = new JwtSecurityTokenHandler().WriteToken(JwtToken),
                IsAuthentticated = true,
                Message = "Successfully"
            };

        }

        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null || await _userManager.FindByNameAsync(model.UserName) is not null)
                return new AuthModel { Message = "Invalid Email or Username,Can't Register Please Try later!" };

            var User = _Mapper.Map<ApplicationUser>(model);

            var Result = await _userManager.CreateAsync(User, model.Password);
            if (!Result.Succeeded)
            {
                StringBuilder errors = new StringBuilder();
                foreach (var error in Result.Errors)
                    errors.Append($"{error.Description} \n");

                return new AuthModel { Message = errors.ToString() };
            }

            await _userManager.AddToRoleAsync(User, Constants.UserRoleString);

            var JwtToken = await CreateJwtToken(User);

            var ActiveRefreshToken = GetRefreshToken();
            User.RefreshTokens.Add(ActiveRefreshToken);

            return new AuthModel
            {
                UserName = User.UserName,
                Email = User.Email,
                IsAuthentticated = true,
                Roles = new List<string> { Constants.UserRoleString },
                Token = new JwtSecurityTokenHandler().WriteToken(JwtToken),
                Expiresion = JwtToken.ValidTo,
                RefreshToken = ActiveRefreshToken.Token,
                RefreshTokenExpiration = ActiveRefreshToken.ExpiresOn,
            };
        }

        public async Task<bool> RevokeTokenAsync(string token)
        {
            var User = await _userManager.Users.SingleOrDefaultAsync(user => user.RefreshTokens.Any(t => t.Token == token));
            if (User == null) return false;

            var RefreshToken = User.RefreshTokens.Single(t => t.Token == token);
            if (!RefreshToken.IsActive) return false;

            RefreshToken.RevokeOn = DateTime.UtcNow;
            await _userManager.UpdateAsync(User);
            return true;
        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser applicationUser)
        {
            var UserClaims = await _userManager.GetClaimsAsync(applicationUser);
            var UserRoles = await _userManager.GetRolesAsync(applicationUser);
            var RoleClaims = new List<Claim>();

            foreach (var role in UserRoles)
            {
                RoleClaims.Add(new Claim("roles", role));
            }

            var Claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,applicationUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,applicationUser.Email),
                new Claim("uid",applicationUser.Id)
            }
                .Union(UserClaims)
                .Union(RoleClaims);

            var SymmetricSecuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jWT.Key));
            var signingCredentials = new SigningCredentials(SymmetricSecuritykey, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                issuer: _jWT.Issuer,
                audience: _jWT.Audience,
                claims: Claims,
                expires: DateTime.Now.AddDays(_jWT.DurationInDays),
                signingCredentials: signingCredentials
                );
            return jwtToken;
        }

        private RefreshToken GetRefreshToken()
        {
            var RandomNumber = new byte[32];
            using var Generator = new RNGCryptoServiceProvider();
            Generator.GetBytes(RandomNumber);
            return new RefreshToken
            {
                CreateOn = DateTime.Now,
                ExpiresOn = DateTime.Now.AddDays(10),
                Token = Convert.ToBase64String(RandomNumber)
            };
        }

    }
}
