using AutoMapper;
using BLL.DTOs.GroupDto;
using BLL.DTOs.UserDto;
using BLL.Services.Contracts;
using DAL.Models;
using DAL.UoW.Contract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class GroupService : IGroupService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public GroupService(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            var config = new MapperConfiguration(c =>
            {
                c.CreateMap<User, UserDTO>().ReverseMap();
                c.CreateMap<Group, GroupDto>().ReverseMap();
                
            });
            config.AssertConfigurationIsValid();
            _mapper = new Mapper(config);
        }
        public async Task CreateGroupAsync(GroupDto group)
        {
            if (group == null)
                throw new NullReferenceException();

            ICollection<User> users = new List<User>();

            foreach (var user in group.Users)
            {
                var userToAdd = _userManager.FindByEmailAsync(user.Email);
                users.Add(userToAdd.Result);
            }

            var mappedGroup = new Group
            {
                GroupName = group.GroupName,
                Users = users
            };

            await _unitOfWork.GroupRepository.AddGroup(mappedGroup);

        }

        public async Task DeleteGroupAsync(int id)
        {
            await _unitOfWork.GroupRepository.DeleteGroup(id);
        }
        public async Task<IEnumerable<GroupDto>> GetAllGroupsAsync(string userEmail)
        {
            var allGroups = await _unitOfWork.GroupRepository.GetGroups();
            allGroups = allGroups.Where(group => group.Users.Any(user => user.Email == userEmail));

            var mappedGroups = from g in allGroups
                               select new GroupDto
                               {
                                   Id = g.Id,
                                   GroupName = g.GroupName,
                                   Users = from u in g.Users
                                           select new UserDTO
                                           {
                                               UserName = u.UserName,
                                               Email = u.Email
                                           }
                               };
            return mappedGroups;
        }

        public async Task<IEnumerable<GroupDto>> GetAllGroupsAsync()
        {
            var allGroups = await _unitOfWork.GroupRepository.GetGroups();
            var mappedGroups = _mapper.Map<IEnumerable<Group>, IEnumerable<GroupDto>>(allGroups);
            return mappedGroups;
        }

        public async Task<GroupDto> GetGroupAsync(int id)
        {
            var group = await _unitOfWork.GroupRepository.GetGroupById(id);

            var mappedGroup = _mapper.Map<Group, GroupDto>(group);
            return mappedGroup;
        }

        public async Task UpdateGroupAsync(int id, List<UserDTO> users)
        {
            var group = await _unitOfWork.GroupRepository.GetGroupById(id);
            List<User> usersToAdd = new List<User>();
            foreach (var user in users)
            {
                var temp = await _userManager.FindByEmailAsync(user.Email);
                usersToAdd.Add(temp);
            }

            if (usersToAdd != null)
            {
                foreach (var user in usersToAdd)
                {
                    group.Users.Add(user);
                }
            }

            await _unitOfWork.GroupRepository.UpdateGroup(group);
        }

        public async Task<IEnumerable<GroupDto>> SearchGroupsAsync(string searchString)
        {
            var groupList = new List<GroupDto>();

            var allGroups = await _unitOfWork.GroupRepository.GetGroups();

            var mappedGroups = from g in allGroups
                           select new GroupDto
                           {
                               Id = g.Id,
                               GroupName = g.GroupName
                           };

            if (searchString == null)
                return mappedGroups;

            var filteredGroups = mappedGroups
                .Where(p => p.GroupName.ToLower().Contains(searchString.ToLower()));

            return filteredGroups;
        }

        public async Task<IEnumerable<UserDTO>> GetUsersAsync(int groupId)
        {
            var group = await _unitOfWork.GroupRepository.GetGroupById(groupId);

            var users = group.Users;

            var mappedUsers = _mapper.Map<IEnumerable<UserDTO>>(users);

            return mappedUsers;
        }
    }
}
