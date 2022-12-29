using AutoMapper;
using BLL.DTOs.UserDto;
using BLL.Services.Contracts;
using DAL.Models;
using DAL.UoW.Contract;
using Microsoft.AspNetCore.Identity;

namespace BLL.Services
{
    public class FriendshipService : IFriendshipService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public FriendshipService(UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(c =>
            {
                c.CreateMap<User, UserDTO>().ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        public async Task<IEnumerable<UserDTO>> GetAllFriendsAsync(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            var friends = await _unitOfWork.FriendshipRepository.GetFriends(user.Id);

            var friendsList = friends.Select(x => x.Friend);

            var mappedFriends = _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(friendsList);

            return mappedFriends;
        }
        public async Task CreateFriendshipAsync(string userEmail, string friendEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            var friend = await _userManager.FindByEmailAsync(friendEmail);

            await _unitOfWork.FriendshipRepository.AddFriend(new Friendship { UserId = user.Id, FriendId = friend.Id });
        }

        public async Task DeleteFriendAsync(string userEmail, string friendEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            var friend = await _userManager.FindByEmailAsync(friendEmail);

            await _unitOfWork.FriendshipRepository.DeleteFriend(new Friendship { UserId = user.Id, FriendId = friend.Id });
        }
    }
}
