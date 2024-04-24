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

        public Task<UserDto> Register(RegisterDto RegisterUser)
        {
            throw new NotImplementedException();
        }
    }
}
