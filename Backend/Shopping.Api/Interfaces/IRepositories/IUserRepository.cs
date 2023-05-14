﻿using Shopping.Api.DTO;
using Shopping.Api.DTO.UserDTO;
using Shopping.Api.Models;

namespace Shopping.Api.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        public Task<User> Authenticate(string email, string password);
        public Task<bool> Register(User newUser);
        public Task<User> Update(UpdateUserDto updatedUser);
        public Task<User> Verify(int userId, string verificationStatus);
        public Task<List<User>> GetSellers();
        public Task<bool> DoesEmailExist(string email);
        public Task<bool> DoesUsernameExist(string username);
        public Task<bool> DoesUserExist(int id);
        public Task<bool> DoesSellerExist(int id);
    }
}
