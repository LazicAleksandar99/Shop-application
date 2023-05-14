﻿using Shopping.Api.DTO.UserDTO;
using Shopping.Api.Models;

namespace Shopping.Api.Interfaces.IServices
{
    public interface IUserService
    {
        public Task<string> Authenticate(LoginUserDto loginUser);
        public Task<string> Register(RegisterUserDto newUser);
        public Task<string> Update(UpdateUserDto updatedUser);
        public Task<bool> Verify(int id, string action);
        public Task<List<GetSellersDto>> GetSellers();
    }
}
