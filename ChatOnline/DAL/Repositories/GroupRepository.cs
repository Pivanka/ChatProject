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
    public class GroupRepository : IGroupRepository
    {
        private readonly ChatDbContext _dbContext;
        public GroupRepository(ChatDbContext chatContext)
        {
            _dbContext = chatContext;
        }

        public async Task AddGroup(Group group)
        {
            if (group == null)
                throw new NullReferenceException();

            await _dbContext.Groups.AddAsync(group);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteGroup(int id)
        {
            var groupToDelete = await _dbContext.Groups.FirstOrDefaultAsync(d => d.Id == id);

            if (groupToDelete == null)
                throw new NullReferenceException();

            _dbContext.Groups.Remove(groupToDelete);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<Group> GetGroupById(int id)
        {
            var group = await _dbContext.Groups
                .Include(x => x.Users)
                .Include(x => x.Messages)
                .FirstOrDefaultAsync(e => e.Id == id);

            if(group == null) 
                throw new NullReferenceException();

            return group;
        }

        public async Task<IEnumerable<Group>> GetGroups()
        {
            return await _dbContext.Groups
                .Include(x => x.Users)
                .Include(x => x.Messages)
                .ToListAsync();
        }

        public async Task UpdateGroup(Group group)
        {
            if (group == null)
                throw new NullReferenceException();

            _dbContext.Groups.Update(group);

            await _dbContext.SaveChangesAsync();
        }
    }
}
