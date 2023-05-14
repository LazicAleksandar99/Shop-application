using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopping.Api.DTO.UserDTO;
using Shopping.Api.Interfaces.IServices;
using System.Data;

namespace Shopping.Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService) 
        {
            _userService = userService;
        }

        //[ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("authentication")]
        [AllowAnonymous]
        //All
        public async Task<IActionResult> Authentication(LoginUserDto loginUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var response = await _userService.Authenticate(loginUser);

            if(response == null)
                return BadRequest("User doesn't exist");

            //return Ulogovanog korisnika da ga odma u store stavim
            return Ok(response);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        //Customer, Seller
        public async Task<IActionResult> Register(RegisterUserDto newUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (newUser.Birthday.Date > DateTime.Now.Date)
                return BadRequest("Date is older than current date");

            var result = await _userService.Register(newUser);
            if (result == "failed")
                return BadRequest("Faild to register user to our shop");
            if (result == "emailexists")
                return BadRequest("Email already registered");
            if (result == "usernameexists")
                return BadRequest("Username already registered");
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            if(id < 1)
                return BadRequest("Invalid id");
            var result = await _userService.GetUserDetails(id);
            if (result == null)
                return BadRequest("No user found");
            return Ok();
        }

        //Seller
        [HttpPatch("update")]
        public async Task<IActionResult> Update(UpdateUserDto updatedUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (updatedUser.Birthday.Date > DateTime.Now.Date)
                return BadRequest("Date is older than current date");

            var response = await _userService.Update(updatedUser);

            if(response == "emailexists")
                return BadRequest("Email already exists");
            if (response == "usernameexists")
                return BadRequest("Username already exists");
            if (response == "nouserfound")
                return BadRequest("User not found");
            if (response == "passwordError")
                return BadRequest("Invalid new or old password");

            return Ok();
        }

        //Administrator
        [HttpPatch("verify/{id}")]
        public async Task<IActionResult> Verify(int id)
        {
            if(id <= 0)
                return BadRequest("Invalid user id");
            if (!await _userService.Verify(id, "Verified"))
                return BadRequest("No users found with this id");
            return Ok();
        }

        //Administrator
        [HttpPatch("deny/{id}")]
        public async Task<IActionResult> Deny(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid user id");
            if (!await _userService.Verify(id, "Denied"))
                return BadRequest("No users found with this id");
            return Ok();
        }

        //Administrator
        [HttpGet("sellers")]
        public async Task<IActionResult> GetSellers()
        {
            return Ok(await _userService.GetSellers());
        }

    }
}
