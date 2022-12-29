using BLL.DTOs.UserDto;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Contracts
{
    public interface IFriendshipService
    {
        Task CreateFriendshipAsync(string userEmail, string friendEmail);
        Task<IEnumerable<UserDTO>> GetAllFriendsAsync(string userEmail);
        Task DeleteFriendAsync(string userEmail, string friendEmail);
    }
}
