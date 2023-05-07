using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopping.Api.DTO;
using Shopping.Api.Interfaces.IServices;

namespace Shopping.Api.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<IActionResult> Authentication(LoginUserDto loginUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var response = await _userService.Authenticate(loginUser);

            if(response == null)
                return BadRequest("User doesn't exist");

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto newUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok();
        }

    }
}
