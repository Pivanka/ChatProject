using BLL.DTOs.GroupDto;
using BLL.DTOs.UserDto;
using BLL.Services.Contracts;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PL.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }
        
        [HttpGet("groups")]
        public async Task<ActionResult<IEnumerable<GroupDto>>> GetGroups()
        {
            try
            {
                var groups = await _groupService.GetAllGroupsAsync();

                if (groups == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(groups);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from database.");
            }
        }
        [HttpGet("usergroups")]
        public async Task<ActionResult<IEnumerable<GroupDto>>> GetGroups(string userEmail)
        {
            try
            {
                var groups = await _groupService.GetAllGroupsAsync(userEmail);

                return Ok(groups);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from database.");
            }
        }

        [HttpGet("group/{id:int}")]
        public async Task<ActionResult<IEnumerable<GroupDto>>> GetGroup(int id)
        {
            try
            {
                var group = await _groupService.GetGroupAsync(id);

                if (group == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(group);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from database.");
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddGroup([FromBody] GroupDto groupToAdd)
        {
            try
            {
                await _groupService.CreateGroupAsync(groupToAdd);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from database.");
            }
        }

        [HttpPut("update/{groupId:int}")]
        public async Task<IActionResult> UpdateGroup(int groupId, [FromBody] List<UserDTO> users)
        {
            try
            {
                await _groupService.UpdateGroupAsync(groupId, users);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from database.");
            }
        }

        [HttpGet("search/{searchString}")]
        public async Task<ActionResult<IEnumerable<GroupDto>>> SearchGroup(string searchString)
        {
            try
            {
                var groups = await _groupService.SearchGroupsAsync(searchString);
                return Ok(groups);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from database.");
            }
        }

        [HttpGet("members/{groupId:int}")]
        public async Task<ActionResult<IEnumerable<GroupDto>>> GetUsersGroup(int groupId)
        {
            try
            {
                var users = await _groupService.GetUsersAsync(groupId);
                return Ok(users);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from database.");
            }
        }
    }
}
