using BLL.DTOs.GroupDto;
using BLL.DTOs.UserDto;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Contracts
{
    public interface IGroupService
    {
        Task CreateGroupAsync(GroupDto group);
        Task<IEnumerable<GroupDto>> GetAllGroupsAsync(string userEmail);
        Task<IEnumerable<GroupDto>> GetAllGroupsAsync();
        Task<GroupDto> GetGroupAsync(int id);
        Task DeleteGroupAsync(int id);
        Task UpdateGroupAsync(int id, List<UserDTO> users);
        Task<IEnumerable<GroupDto>> SearchGroupsAsync(string searchString);
        Task<IEnumerable<UserDTO>> GetUsersAsync(int groupId);
    }
}
