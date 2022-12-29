using DAL.Context;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.DAL.Tests.FriendshipRepo
{
    public class FriendshipSeedDataFixture : IDisposable
    {
        public FriendshipSeedDataFixture()
        {
            ChatDbContext.Friendships.Add(new Friendship
            {
                UserId = 1,
                FriendId = 2
            });
            ChatDbContext.Friendships.Add(new Friendship
            {
                UserId = 2,
                FriendId = 1
            });
            ChatDbContext.Friendships.Add(new Friendship
            {
                UserId = 1,
                FriendId = 3
            });
            ChatDbContext.Friendships.Add(new Friendship
            {
                UserId = 3,
                FriendId = 1
            });

            ChatDbContext.Users.Add(new User
            {
                Id = 1,
                UserName = "test1",
                Email = "test1@test.com"
            });
            ChatDbContext.Users.Add(new User
            {
                Id = 2,
                UserName = "test2",
                Email = "test2@test.com"
            });
            ChatDbContext.Users.Add(new User
            {
                Id = 3,
                UserName = "test3",
                Email = "test3@test.com"
            });

            ChatDbContext.SaveChanges();
        }

        public ChatDbContext ChatDbContext { get; } = new ChatDbContext(
            new DbContextOptionsBuilder<ChatDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);

        public void Dispose()
        {
            ChatDbContext.Database.EnsureDeleted();
            ChatDbContext.Dispose();
        }
    }
}
