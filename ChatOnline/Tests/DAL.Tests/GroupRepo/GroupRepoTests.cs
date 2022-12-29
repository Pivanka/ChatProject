using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.DAL.Tests.GroupRepo
{
    [TestFixture]
    public class GroupRepoTests
    {
        private static GroupRepository _groupRepository;

        [Test]
        public void AddGroup_Null()
        {
            using var fixture = new GroupSeedDataFixture();
            _groupRepository = new GroupRepository(fixture.ChatDbContext);

            // Arrange
            Group nullGroup = null;
            var expectedException = typeof(NullReferenceException);

            // Act
            var actualException = Assert.CatchAsync(
                async () => await _groupRepository.AddGroup(nullGroup));

            // Assert
            Assert.IsNotNull(actualException, "Method doesn't throw any exceptions.");
            Assert.That(actualException.GetType(), Is.EqualTo(expectedException),
                "Method doesn't throw null exception.");
        }

        [Test]
        public async Task AddGroup_Not_Null()
        {
            using var fixture = new GroupSeedDataFixture();
            _groupRepository = new GroupRepository(fixture.ChatDbContext);

            // Arrange
            var group = new Group
            {
                GroupName = "Test"
            };
            var expectedCount = 3;

            // Act
            await _groupRepository.AddGroup(group);
            var actualCount = fixture.ChatDbContext.Groups.Count();

            //Assert
            Assert.That(actualCount, Is.EqualTo(expectedCount), "Group wasn't added.");
        }

        [Test]
        public void DeleteGroup_Not_Existing_Id()
        {
            using var fixture = new GroupSeedDataFixture();
            _groupRepository = new GroupRepository(fixture.ChatDbContext);

            // Arrange
            var notExistingId = -1;
            var expectedException = typeof(NullReferenceException);

            // Act
            var actualException = Assert.CatchAsync(
                async () => await _groupRepository.DeleteGroup(notExistingId));

            // Assert
            Assert.That(actualException.GetType(), Is.EqualTo(expectedException), "Group was deleted.");
        }

        [Test]
        public async Task DeleteGroup_Existing_Id()
        {
            using var fixture = new GroupSeedDataFixture();
            _groupRepository = new GroupRepository(fixture.ChatDbContext);

            // Arrange
            var existingId = 1;
            var expectedCount = 1;

            // Act
            await _groupRepository.DeleteGroup(existingId);
            var actualCount = fixture.ChatDbContext.Groups.Count();

            //Assert
            Assert.That(actualCount, Is.EqualTo(expectedCount), "Group wasn't deleted.");
        }

        [Test]
        public async Task GetGroups()
        {
            using var fixture = new GroupSeedDataFixture();
            _groupRepository = new GroupRepository(fixture.ChatDbContext);

            // Arrange
            var expectedCount = 2;

            // Act
            var actualCount = (await _groupRepository.GetGroups()).Count();

            // Assert
            Assert.That(actualCount, Is.EqualTo(expectedCount), "Groups count wasn't equal to expected.");
        }

        [Test]
        public void GetGroupById_Not_Existing_Id()
        {
            using var fixture = new GroupSeedDataFixture();
            _groupRepository = new GroupRepository(fixture.ChatDbContext);

            // Arrange
            var notExistingId = -1;
            var expectedException = typeof(NullReferenceException);

            // Act
            var actualException = Assert.CatchAsync(async () => await _groupRepository.GetGroupById(notExistingId));

            // Assert
            Assert.That(actualException.GetType(), Is.EqualTo(expectedException), "Group wasn't null.");
        }

        [Test]
        public async Task GetGroupById_Existing_Id()
        {
            using var fixture = new GroupSeedDataFixture();
            _groupRepository = new GroupRepository(fixture.ChatDbContext);

            // Arrange
            var existingId = 1;
            var expectedGroupName = "Friends";

            // Act
            var actualProductName = (await _groupRepository.GetGroupById(existingId)).GroupName;

            // Assert
            Assert.That(actualProductName, Is.EqualTo(expectedGroupName), "Group was not equal.");
        }
        [Test]
        public void UpdateGroup_Null()
        {
            using var fixture = new GroupSeedDataFixture();
            _groupRepository = new GroupRepository(fixture.ChatDbContext);

            // Arrange 
            Group nullGroup = null;
            var expectedException = typeof(NullReferenceException);

            // Act
            var actualException = Assert.CatchAsync(async () => await _groupRepository.UpdateGroup(nullGroup));

            // Assert
            Assert.That(actualException.GetType(), Is.EqualTo(expectedException), "Group was updated.");
        }

        [Test]
        public async Task UpdateGroup_Not_Null()
        {
            using var fixture = new GroupSeedDataFixture();
            _groupRepository = new GroupRepository(fixture.ChatDbContext);

            // Arrange
            var group = await _groupRepository.GetGroupById(1);
            var expectedName = "Friends";
            group.GroupName = expectedName;

            // Act
            await _groupRepository.UpdateGroup(group);
            var actualName = (await _groupRepository.GetGroupById(1)).GroupName;

            // Assert
            Assert.That(actualName, Is.EqualTo(expectedName), "Groups' names aren't equal.");
        }
    }
}
