using BLL.DTOs.MessageDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Contracts
{
    public interface IMessageService
    {
        Task CreateMessageAsync(MessageToAddDto message);
        Task<IEnumerable<MessageDto>> GetAllMessagesAsync(int id);
        Task<MessageDto> GetMessageAsync(int id);
        Task ReplyMessageAsync(MessageReplyDto message);
        Task DeleteMessageAsync(int id);
        Task DeleteForUserMessageAsync(int id);
        Task UpdateMessageAsync(MessageUpdateDto message);
    }
}
