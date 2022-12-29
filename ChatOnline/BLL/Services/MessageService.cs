using BLL.DTOs.MessageDto;
using BLL.Services.Contracts;
using DAL.Models;
using DAL.UoW.Contract;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BLL.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public MessageService(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            var config = new MapperConfiguration(c =>
            {
                c.CreateMap<Message, MessageDto>()
                .ForPath(m => m.UserName,
                    opt => opt.MapFrom(u => u.User.UserName))
                .ReverseMap();

                c.CreateMap<Message, MessageToAddDto>()
                .ForPath(m => m.UserEmail,
                    opt => opt.MapFrom(u => u.User.Email));
            });
            _mapper = new Mapper(config);
        }

        public async Task CreateMessageAsync(MessageToAddDto message)
        {
            if (message == null)
                throw new ArgumentNullException();

            var user = await _userManager.FindByEmailAsync(message.UserEmail);

            Message messageToAdd = new Message
            {
                Text = message.Text,
                CreatedAt = message.CreatedAt,
                UserId = user.Id,
                GroupId = message.GroupId
            };

            await _unitOfWork.MessageRepository.AddMessage(messageToAdd);
        }

        public async Task DeleteForUserMessageAsync(int id)
        {
            var message = await _unitOfWork.MessageRepository.GetMessageById(id);

            if (message == null)
                throw new ArgumentNullException();

            message.DeletedForUser = true;

            await _unitOfWork.MessageRepository.UpdateMessage(message);
        }

        public async Task DeleteMessageAsync(int id)
        {
            await _unitOfWork.MessageRepository.DeleteMessage(id);
        }

        public async Task<IEnumerable<MessageDto>> GetAllMessagesAsync(int id)
        {
            var allMessages = await _unitOfWork.MessageRepository.GetMessages();

            var filteredMessages = allMessages.Where(
                x => x.GroupId == id);

            var mappedMessages = _mapper.Map<IEnumerable<MessageDto>>(filteredMessages);
            return mappedMessages;
        }

        public async Task<MessageDto> GetMessageAsync(int id)
        {
            var message = await _unitOfWork.MessageRepository.GetMessageById(id);

            var mappedMessage = _mapper.Map<MessageDto>(message);
            return mappedMessage;
        }

        public async Task ReplyMessageAsync(MessageReplyDto message)
        {
            if (message == null)
                throw new ArgumentNullException();

            var user = await _userManager.FindByEmailAsync(message.UserEmail);

            Message messageToAdd = new Message
            {
                Text = message.Text,
                CreatedAt = message.CreatedAt,
                UserId = user.Id,
                GroupId = message.GroupId,
                ParentMessageId = message.ParentMessageId,
                ParentMessage = _unitOfWork.MessageRepository.GetMessageById(message.ParentMessageId).Result.Text
            };

            await _unitOfWork.MessageRepository.AddMessage(messageToAdd);
        }

        public async Task UpdateMessageAsync(MessageUpdateDto message)
        {
            var messageToUpdate = await _unitOfWork.MessageRepository.GetMessageById(message.Id);

            if (messageToUpdate == null) throw new NullReferenceException();

            messageToUpdate.UpdatedAt = message.UpdatedAt;
            messageToUpdate.Text = message.Text;

            await _unitOfWork.MessageRepository.UpdateMessage(messageToUpdate);
        }
    }
}
