using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.DAL.Tests.FriendshipRepo
{
    [TestFixture]
    public class FriendshipRepoTests
    {
        private static FriendshipRepository _friendshipRepository;

        [Test]
        public void AddFriend_Null()
        {
            using var fixture = new FriendshipSeedDataFixture();
            _friendshipRepository = new FriendshipRepository(fixture.ChatDbContext);

            // Arrange
            Friendship nullFriendship = null;
            var expectedException = typeof(NullReferenceException);

            // Act
            var actualException = Assert.CatchAsync(
                async () => await _friendshipRepository.AddFriend(nullFriendship));

            // Assert
            Assert.IsNotNull(actualException, "Method doesn't throw any exceptions.");
            Assert.That(actualException.GetType(), Is.EqualTo(expectedException),
                "Method doesn't throw null exception.");
        }

        [Test]
        public async Task AddFriend_Not_Null()
        {
            using var fixture = new FriendshipSeedDataFixture();
            _friendshipRepository = new FriendshipRepository(fixture.ChatDbContext);

            // Arrange
            Friendship friendship = new Friendship
            {
                UserId = 2,
                FriendId = 3
            };
            var expectedCount = 6;

            // Act
            await _friendshipRepository.AddFriend(friendship);
            var actualCount = fixture.ChatDbContext.Friendships.Count();

            //Assert
            Assert.That(actualCount, Is.EqualTo(expectedCount), "Friendship wasn't added.");
        }

        [Test]
        public void DeleteFriend_Null_Friendship()
        {
            using var fixture = new FriendshipSeedDataFixture();
            _friendshipRepository = new FriendshipRepository(fixture.ChatDbContext);

            // Arrange
            Friendship notExisting = null;
            var expectedException = typeof(NullReferenceException);

            // Act
            var actualException = Assert.CatchAsync(
                async () => await _friendshipRepository.DeleteFriend(notExisting));

            // Assert
            Assert.That(actualException.GetType(), Is.EqualTo(expectedException), "Friendship was deleted.");
        }


        [Test]
        public async Task GetFriends()
        {
            using var fixture = new FriendshipSeedDataFixture();
            _friendshipRepository = new FriendshipRepository(fixture.ChatDbContext);

            // Arrange
            int userId = 1;
            var expectedCount = 2;

            // Act
            var actualCount = (await _friendshipRepository.GetFriends(userId)).Count();

            // Assert
            Assert.That(actualCount, Is.EqualTo(expectedCount), "Friends count wasn't equal to expected.");
        }
    }
}
