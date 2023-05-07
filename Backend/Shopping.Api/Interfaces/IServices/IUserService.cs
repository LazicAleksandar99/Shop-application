using Shopping.Api.DTO;

namespace Shopping.Api.Interfaces.IServices
{
    public interface IUserService
    {
        public Task<string> Authenticate(LoginUserDto loginUser);
        public Task<string> Register(RegisterUserDto newUser);
    }
}
