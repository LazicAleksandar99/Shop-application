using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shopping.Api.DTO.UserDTO;
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
        private readonly IMapper _mapper;
        public UserService(IConfiguration configuration, IUserRepository userRepository, IMapper mapper)
        {
            _configuration = configuration;
            _userRepo = userRepository;
            _mapper = mapper;
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

            if (!await _userRepo.DoesEmailExist(newUser.Email))
                return "emailexists";

            if (!await _userRepo.DoesUsernameExist(newUser.Username))
                return "usernameexists";

            var user = _mapper.Map<User>(newUser);
            user.Password = passwordHash;
            user.PasswordKey = passwordKey;
            user.VerificationStatus = newUser.Role == "Customer" ? "Verified" : "Pending";

            var response = await _userRepo.Register(user);

            if (!response)
                return "failed"; 

            return "successful";
        }

        public async Task<string> Update(UpdateUserDto updatedUser)
        {
            if (!await _userRepo.DoesEmailExist(updatedUser.Email))
                return "emailexists";

            if (!await _userRepo.DoesUsernameExist(updatedUser.Username))
                return "usernameexists";

            if (!await _userRepo.DoesUserExist(updatedUser.Id))
                return "nouserfound";

            if (String.IsNullOrWhiteSpace(updatedUser.Newpassword) && String.IsNullOrWhiteSpace(updatedUser.Oldpassword))
            {
                await _userRepo.Update(updatedUser);
            }
            else if (!String.IsNullOrWhiteSpace(updatedUser.Newpassword) && !String.IsNullOrWhiteSpace(updatedUser.Oldpassword))
            {
                await _userRepo.Update(updatedUser);
            }
            else
                return "passwordError";

            return "updated";
        }

        public async Task<bool> Verify(int id, string verificationStatus)
        {
            if (!await _userRepo.DoesSellerExist(id))
                return false;

            await Verify(id, verificationStatus);
            return true;
        }

        //U DTO DRUZE
        public async Task<List<User>> GetSellers()
        {
            return await _userRepo.GetSellers();
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
