using Core.DataTransferObjects.User;
using Core.Interfaces.Services;
using Core.Models.User;
using Microsoft.AspNetCore.Identity;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly TokenService tokenService;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, TokenService tokenService)
        {
            _UserManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
        }

        public async Task<UserDto> Login(LoginDto LoginUser)
        {
            var User = await _UserManager.FindByEmailAsync(LoginUser.Email);
            if(User is not null)
            {
                var res = await signInManager.CheckPasswordSignInAsync(User, LoginUser.Password,false);
                if (res.Succeeded)
                    return new UserDto
                    {
                        Email = LoginUser.Email,
                        DisplayName = LoginUser.Email,
                        Token = tokenService.GenerateToken(User)
                    };
            }
            return null;
        }

        public async Task<UserDto> Register(RegisterDto RegisterUser)
        {
            var user = _UserManager.FindByEmailAsync(RegisterUser.Email);
            if (user is not null) throw new Exception("this user already registered");

            var appUser = new ApplicationUser()
            {
                DisplayName = RegisterUser.DisplayName,
                Email = RegisterUser.Email,
                UserName = RegisterUser.Email
            };
            var res = await _UserManager.CreateAsync(appUser);
            if (!res.Succeeded) throw new Exception("error while creating user");

            var returnUser = new UserDto()
            {
                DisplayName = appUser.DisplayName,
                Email = appUser.Email,
                Token = tokenService.GenerateToken(appUser)
            };
            return returnUser;
        }
    }
}
