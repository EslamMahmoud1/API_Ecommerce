using Core.DataTransferObjects.User;

namespace Core.Interfaces.Services
{
    public interface IUserService
    {
        public Task<UserDto> Login(LoginDto LoginUser);
        public Task<UserDto> Register(RegisterDto RegisterUser);
    }
}
