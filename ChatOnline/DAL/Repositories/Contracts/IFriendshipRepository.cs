using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Contracts
{
    public interface IFriendshipRepository
    {
        Task<IEnumerable<Friendship>> GetFriends(int userId);
        Task AddFriend(Friendship friendship);
        Task DeleteFriend(Friendship friendship);
    }
}
