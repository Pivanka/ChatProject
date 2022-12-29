using BLL.DTOs.UserDto;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Contracts
{
    public interface IUserService
    {
        Task<UserManagerResponse> RegisterUserAsync(SignUp user);
        Task<UserManagerResponse> LoginUserAsync(SignIn user);
        Task Logout();
        Task<IEnumerable<UserDTO>> GetUsersAsync();
        Task<UserDTO> GetUsersByEmailAsync(string email);
    }
}
