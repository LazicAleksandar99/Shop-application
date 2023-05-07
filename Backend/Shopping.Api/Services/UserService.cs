using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shopping.Api.DTO;
using Shopping.Api.Interfaces.IRepositories;
using Shopping.Api.Interfaces.IServices;
using Shopping.Api.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace Shopping.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepo;
        public UserService(IConfiguration configuration, IUserRepository userRepository)
        {
            this._configuration = configuration;
            this._userRepo = userRepository;  
        }

        public async Task<string> Authenticate(LoginUserDto loginUser)
        {
            var user = await _userRepo.Authenticate(loginUser.Email,loginUser.Password);

            if (user == null)
                return null;

            var token = CreateJWT(user);
            return token;
        }

        public async Task<string> Register(RegisterUserDto newUser)
        {
            byte[] passwordHash, passwordKey;

            using (var hmac = new HMACSHA512())
            {
                passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(newUser.Password));

            }
            //provjera za Username i Email
            User user = new User();
            user.Username = newUser.Username;
            user.Password = passwordHash;
            user.PasswordKey = passwordKey;
            user.Address = newUser.Address;
            user.Birthday = newUser.Birthday;
            user.Email = newUser.Email;
            user.FirstName = newUser.FirstName;
            user.LastName = newUser.LastName;
            user.Role = newUser.Role;
            user.VerificationStatus = newUser.Role == "Customer" ? "Verified" : "Pending";

            var response = await _userRepo.Register(user);

            if (!response)
                return "failed"; 

            return "successful";
        }





        private string CreateJWT(User user)
        {
            var secretKey = _configuration.GetSection("AppSettings:Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(secretKey!));

            var claims = new Claim[] {
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Role,user.Role.ToString())
            };

            var signingCredentials = new SigningCredentials(
                    key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
