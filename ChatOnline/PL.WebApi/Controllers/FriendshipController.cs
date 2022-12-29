using BLL.DTOs.UserDto;
using BLL.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PL.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FriendshipController : ControllerBase
    {
        private readonly IFriendshipService _friendshipService;

        public FriendshipController(IFriendshipService friendshipService)
        {
            this._friendshipService = friendshipService;
        }

        [HttpPost("addfriend")]
        public async Task<IActionResult> AddFriend(string userEmail, string friendEmail)
        {
            try
            {
                await _friendshipService.CreateFriendshipAsync(userEmail, friendEmail);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from database.");
            }
        }

        [HttpGet("friends")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetFriends(string userEmail)
        {
            try
            {
                var result = await _friendshipService.GetAllFriendsAsync(userEmail);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from database.");
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteFriend(string userEmail, string friendEmail)
        {
            try
            {
                await _friendshipService.DeleteFriendAsync(userEmail, friendEmail);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from database.");
            }
        }
    }
}
