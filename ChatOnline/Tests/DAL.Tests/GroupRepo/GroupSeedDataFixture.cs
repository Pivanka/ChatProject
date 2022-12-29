using DAL.Context;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.DAL.Tests.GroupRepo
{
    public class GroupSeedDataFixture : IDisposable
    {
        public GroupSeedDataFixture()
        {
            ChatDbContext.Groups.Add(new Group
            {
                GroupName = "Friends",
                Users = new List<User>(),
                Messages = new List<Message>()
            });
            ChatDbContext.Groups.Add(new Group
            {
                GroupName = "Work Chat",
                Users = new List<User>(),
                Messages = new List<Message>()
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
