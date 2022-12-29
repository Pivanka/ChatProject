using DAL.Context;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.DAL.Tests.MessageRepo
{
    public class MessageSeedDataFixture : IDisposable
    {
        public MessageSeedDataFixture()
        {
            ChatDbContext.Messages.Add(new Message
            {
                Text = "Test",
                CreatedAt = DateTime.Now,
                GroupId = 1,
                UserId = 1,
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
            ChatDbContext.Groups.Add(new Group
            {
                Id = 1,
                GroupName = "Friends",
            });
            ChatDbContext.Groups.Add(new Group
            {
                Id = 2,
                GroupName = "Work Chat",
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
