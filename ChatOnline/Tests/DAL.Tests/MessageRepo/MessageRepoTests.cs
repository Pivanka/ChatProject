using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.DAL.Tests.MessageRepo
{
    [TestFixture]
    public class MessageRepoTests
    {
        private static MessageRepository _messageRepository;

        [Test]
        public void AddMessage_Null()
        {
            using var fixture = new MessageSeedDataFixture();
            _messageRepository = new MessageRepository(fixture.ChatDbContext);

            // Arrange
            Message nullMessage = null;
            var expectedException = typeof(NullReferenceException);

            // Act
            var actualException = Assert.CatchAsync(
                async () => await _messageRepository.AddMessage(nullMessage));

            // Assert
            Assert.IsNotNull(actualException, "Method doesn't throw any exceptions.");
            Assert.That(actualException.GetType(), Is.EqualTo(expectedException),
                "Method doesn't throw null exception.");
        }

        [Test]
        public async Task AddMessage_Not_Null()
        {
            using var fixture = new MessageSeedDataFixture();
            _messageRepository = new MessageRepository(fixture.ChatDbContext);

            // Arrange
            var message = new Message
            {
                Text = "Test2",
                CreatedAt = DateTime.Now,
                GroupId = 1,
                UserId = 1,
            };
            var expectedCount = 2;

            // Act
            await _messageRepository.AddMessage(message);
            var actualCount = fixture.ChatDbContext.Messages.Count();

            //Assert
            Assert.That(actualCount, Is.EqualTo(expectedCount), "Message wasn't added.");
        }

        [Test]
        public void DeleteMessage_Not_Existing_Message()
        {
            using var fixture = new MessageSeedDataFixture();
            _messageRepository = new MessageRepository(fixture.ChatDbContext);

            // Arrange
            var notExistingId = -1;
            var expectedException = typeof(NullReferenceException);

            // Act
            var actualException = Assert.CatchAsync(
                async () => await _messageRepository.DeleteMessage(notExistingId));

            // Assert
            Assert.That(actualException.GetType(), Is.EqualTo(expectedException), "Message was deleted.");
        }

        [Test]
        public async Task DeleteMessage_Existing_Message()
        {
            using var fixture = new MessageSeedDataFixture();
            _messageRepository = new MessageRepository(fixture.ChatDbContext);

            // Arrange
            var existingId = 1;
            var expectedCount = 0;

            // Act
            await _messageRepository.DeleteMessage(existingId);
            var actualCount = fixture.ChatDbContext.Messages.Count();

            //Assert
            Assert.That(actualCount, Is.EqualTo(expectedCount), "Message wasn't deleted.");
        }

        [Test]
        public async Task GetMessages()
        {
            using var fixture = new MessageSeedDataFixture();
            _messageRepository = new MessageRepository(fixture.ChatDbContext);

            // Arrange
            var expectedCount = 1;

            // Act
            var actualCount = (await _messageRepository.GetMessages()).Count();

            // Assert
            Assert.That(actualCount, Is.EqualTo(expectedCount), "Messages count wasn't equal to expected.");
        }

        [Test]
        public void GetMessageById_Not_Existing_Id()
        {
            using var fixture = new MessageSeedDataFixture();
            _messageRepository = new MessageRepository(fixture.ChatDbContext);

            // Arrange
            var notExistingId = -1;
            var expectedException = typeof(NullReferenceException);

            // Act
            var actualException = Assert.CatchAsync(async () => await _messageRepository.GetMessageById(notExistingId));

            // Assert
            Assert.That(actualException.GetType(), Is.EqualTo(expectedException), "Message wasn't null.");
        }

        [Test]
        public async Task GetMessageById_Existing_Id()
        {
            using var fixture = new MessageSeedDataFixture();
            _messageRepository = new MessageRepository(fixture.ChatDbContext);

            // Arrange
            var existingId = 1;
            var expectedText = "Test";

            // Act
            var actualText = (await _messageRepository.GetMessageById(existingId)).Text;

            // Assert
            Assert.That(actualText, Is.EqualTo(expectedText), "Message wasn't equal.");
        }

        [Test]
        public void UpdateMessage_Null()
        {
            using var fixture = new MessageSeedDataFixture();
            _messageRepository = new MessageRepository(fixture.ChatDbContext);

            // Arrange 
            Message nullMessage = null;
            var expectedException = typeof(NullReferenceException);

            // Act
            var actualException = Assert.CatchAsync(async () => await _messageRepository.UpdateMessage(nullMessage));

            // Assert
            Assert.That(actualException.GetType(), Is.EqualTo(expectedException), "Message was updated.");
        }

        [Test]
        public async Task UpdateMessage_Not_Null()
        {
            using var fixture = new MessageSeedDataFixture();
            _messageRepository = new MessageRepository(fixture.ChatDbContext);

            // Arrange
            var message = await _messageRepository.GetMessageById(1);
            var expectedText = "Updated text";
            message.Text = expectedText;

            // Act
            await _messageRepository.UpdateMessage(message);
            var actualText = (await _messageRepository.GetMessageById(1)).Text;

            // Assert
            Assert.That(actualText, Is.EqualTo(expectedText), "Message's text isn't equal.");
        }
    }
}
