using Shopping.Api.DTO;
using Shopping.Api.Models;

namespace Shopping.Api.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        public Task<User> Authenticate(string email, string password);
        public Task<bool> Register(User newUser);

    }
}
