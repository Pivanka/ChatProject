using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Contracts
{
    public interface IMessageRepository
    {
        Task<IEnumerable<Message>> GetMessages();
        Task<Message> GetMessageById(int id);
        Task AddMessage(Message message);
        Task UpdateMessage(Message message);
        Task DeleteMessage(int id);
    }
}
