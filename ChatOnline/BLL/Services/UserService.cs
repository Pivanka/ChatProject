using AutoMapper;
using BLL.DTOs.UserDto;
using BLL.Services.Contracts;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private IConfiguration _configuration;
        private readonly IMapper _mapper;
        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
            var config = new MapperConfiguration(c =>
            {
                c.CreateMap<User, UserDTO>().ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public async Task<UserManagerResponse> LoginUserAsync(SignIn model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return new UserManagerResponse
                {
                    Message = "There is no user that such credentials",
                    IsSuccess = false
                };

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!isPasswordCorrect)
                return new UserManagerResponse
                {
                    Message = "Invalid password",
                    IsSuccess = false
                };

            //access token
            var claims = new[]
            {
                new Claim("Email", model.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSettings:Issuer"],
                audience: _configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));


            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            await _signInManager.SignInAsync(user, true);

            return new UserManagerResponse
            {
                Message = tokenAsString,
                IsSuccess = true,
                ExpireDate = token.ValidTo,
                User = new UserDTO
                {
                    UserName = user.UserName,
                    Email = user.Email,
                }
            };
        }

        public async Task<UserManagerResponse> RegisterUserAsync(SignUp user)
        {
            if (user == null)
                throw new NullReferenceException("Register model is null");

            if (_userManager.FindByEmailAsync(user.Email).Result != null)
                return new UserManagerResponse
                {
                    Message = "Email is already register",
                    IsSuccess = false
                };

            if (user.Password != user.ConfirmPassword)
                return new UserManagerResponse
                {
                    Message = "Confirm password does not match the password",
                    IsSuccess = false
                };

            var userToAdd = new User
            {
                UserName = user.UserName,
                Email = user.Email
            };

            var result = await _userManager.CreateAsync(userToAdd, user.Password);

            if (result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Message = "User created successfully",
                    IsSuccess = true
                };
            }

            return new UserManagerResponse
            {
                Message = "User did not create",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IEnumerable<UserDTO>> GetUsersAsync()
        {
            var users = _userManager.Users;

            var mappedUsers = _mapper.Map< IEnumerable<User>, IEnumerable<UserDTO>>(await users.ToListAsync());

            return mappedUsers;
        }

        public async Task<UserDTO> GetUsersByEmailAsync(string email)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);

            var mappedUser = _mapper.Map<UserDTO>(user);

            return mappedUser;
        }
    }
}
