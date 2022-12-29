using BLL.DTOs.MessageDto;
using BLL.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PL.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [Authorize]
        [HttpGet("chat")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetChat(int groupId)
        {
            try
            {
                var groupMessages = await _messageService.GetAllMessagesAsync(groupId);

                if (groupMessages == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(groupMessages);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from database.");
            }
        }

        [HttpPost("add")]
        public async Task<ActionResult<MessageDto>> AddMessage([FromBody] MessageToAddDto messageToAdd)
        {
            try
            {
                await _messageService.CreateMessageAsync(messageToAdd);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("reply")]
        public async Task<IActionResult> ReplyMessage([FromBody] MessageReplyDto messageToAdd)
        {
            try
            {
                await _messageService.ReplyMessageAsync(messageToAdd);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            try
            {
                await _messageService.DeleteMessageAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("delete/for/user")]
        public async Task<IActionResult> DeleteMessageForUser([FromBody] int id)
        {
            try
            {
                await _messageService.DeleteForUserMessageAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateMessage([FromBody] MessageUpdateDto messageUpdateDto)
        {
            try
            {
                await _messageService.UpdateMessageAsync(messageUpdateDto);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}