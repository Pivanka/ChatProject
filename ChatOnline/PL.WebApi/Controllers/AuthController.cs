using BLL.DTOs.UserDto;
using BLL.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PL.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] SignUp model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userService.RegisterUserAsync(model);

                    if (result.IsSuccess)
                        return Ok(result);

                    return BadRequest(result);
                }
                return BadRequest("Model is not valid"); 
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                 "Error retrieving data from database.");
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] SignIn model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userService.LoginUserAsync(model);

                    if (result.IsSuccess)
                        return Ok(result);

                    return BadRequest(result);
                }
                return BadRequest("Some credentials are not valid"); 
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                 "Error retrieving data from database.");
            }
        }

        [HttpGet("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _userService.Logout();
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("users/all")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            try
            {
                var users = await _userService.GetUsersAsync();

                if (users == null)
                    return NotFound();

                return Ok(users);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from database.");
            }
        }

        [HttpGet("user/{userEmail}")]
        [Authorize]
        public async Task<ActionResult<UserDTO>> GetUser(string userEmail)
        {
            try
            {
                var user = await _userService.GetUsersByEmailAsync(userEmail);

                if (user == null)
                    return NotFound();

                return Ok(user);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from database.");
            }
        }
    }
}
