using DAL.Context;
using DAL.Models;
using DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ChatDbContext _dbContext;
        public MessageRepository(ChatDbContext chatContext)
        {
            _dbContext = chatContext;
        }

        public async Task<IEnumerable<Message>> GetMessages()
        {
            return await _dbContext.Messages
                .Include(u => u.User)
                .Include(u => u.Group)
                .ToListAsync();
        }

        public async Task<Message> GetMessageById(int id)
        {
            var message = await _dbContext.Messages
                .Include(u => u.User)
                .Include(u => u.Group)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (message == null) throw new NullReferenceException();

            return message;
        }

        public async Task AddMessage(Message message)
        {
            if (message == null)
                throw new NullReferenceException();

            _dbContext.Messages.Add(message);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateMessage(Message message)
        {
            if (message == null)
                throw new NullReferenceException();

            _dbContext.Update(message);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteMessage(int id)
        {
            var item = await _dbContext.Messages.FirstOrDefaultAsync(c => c.Id == id);

            if (item == null)
                throw new NullReferenceException();

            _dbContext.Messages.Remove(item);
            await _dbContext.SaveChangesAsync();
        }
    }
}
