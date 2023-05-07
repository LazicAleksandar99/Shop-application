using Backend.Helpers;
using Microsoft.EntityFrameworkCore;
using Shopping.Api.Interfaces.IRepositories;
using Shopping.Api.Models;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace Shopping.Api.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _data;
        private readonly AuthenticationHelper _helper;

        public UserRepository (DataContext data)
        {
            _data = data;
            _helper = new AuthenticationHelper();
        }

        public async Task<User> Authenticate(string email, string password)
        {
            var user = await _data.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null || user.PasswordKey == null)
                return null!;

            if (!_helper.MatchPasswordHash(password, user.Password!, user.PasswordKey))
                return null!;

            return user;
        }

        public async Task<bool> Register(User newUser)
        {
            try
            {
                _data.Users.Add(newUser);
                await _data.SaveChangesAsync();
                return true;
            }
            catch( Exception ex)
            {
                return false;
            }
        }

    }
}
