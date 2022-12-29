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
    public class FriendshipRepository : IFriendshipRepository
    {
        private readonly ChatDbContext _dbContext;
        public FriendshipRepository(ChatDbContext chatContext)
        {
            _dbContext = chatContext;
        }
        public async Task<IEnumerable<Friendship>> GetFriends(int userId)
        {
            return await _dbContext.Friendships.Where(user => user.UserId == userId).Include(x => x.Friend).ToListAsync();
        }
        public async Task AddFriend(Friendship friendship)
        {
            if (friendship == null)
                throw new NullReferenceException();

            _dbContext.Friendships.Add(friendship);
            Friendship reversedFriendship = new Friendship
            {
                UserId = friendship.FriendId,
                FriendId = friendship.UserId
            };
            _dbContext.Friendships.Add(reversedFriendship);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteFriend(Friendship friendship)
        {
            if (friendship == null)
                throw new NullReferenceException();

            _dbContext.Friendships.Remove(friendship);
            Friendship reversedFriendship = new Friendship
            {
                UserId = friendship.FriendId,
                FriendId = friendship.UserId
            };
            _dbContext.Friendships.Remove(reversedFriendship);
            await _dbContext.SaveChangesAsync();
        }
    }
}
