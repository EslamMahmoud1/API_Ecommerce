using API_Project.Error;
using Core.DataTransferObjects.User;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto user)
        {
            var UserDto = await _userService.Login(user);
            return UserDto;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto user)
        {
            var UserDto = await _userService.Register(user);
            return UserDto is not null ? Ok(UserDto) : Unauthorized(new ErrorResponseBody(401));
        }
    }
}
