using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Contracts
{
    public interface IGroupRepository
    {
        Task<IEnumerable<Group>> GetGroups();
        Task<Group> GetGroupById(int id);
        Task AddGroup(Group group);
        Task DeleteGroup(int id);
        Task UpdateGroup(Group group);
    }
}
