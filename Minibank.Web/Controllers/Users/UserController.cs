using Microsoft.AspNetCore.Mvc;
using Minibank.Core.Domains.AccountsBank.Services;
using Minibank.Core.Domains.Users;
using Minibank.Core.Domains.Users.Services;
using Minibank.Web.Controllers.Users.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Minibank.Web.Controllers.Users
{
    [ApiController]
    [Route("/api/users")]
    public class UserController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Создать пользователя
        /// </summary>
        /// <param name="login"></param>
        /// <param name="email"></param>
        [HttpPost("/{login, email}")]
        public async Task CreateUserAsync(UserUpdateDto user)
        {
            UserDto model = new UserDto() { Login = user.Login, Email = user.Email };
            await _userService.CreateAsync(new User
            {
                Email = model.Email,
                Login = model.Login
            });
        }
        /// <summary>
        /// Вернуть список пользователей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userService.GetAllAsync();
        }
        /// <summary>
        /// Update email
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        [HttpPut("{id}/{Login, Email}")]
        public async Task UpdateUserAsync(string id, UserUpdateDto model)
        {
            await _userService.UpdateUserAsync(id, new User() { Email = model.Email, Login = model.Login});
        }
        /// <summary>
        /// Удалить пользователя
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("/{id}")]
        public async Task DeleteUserAsync(string id)
        {
            await _userService.DeleteAsync(id);
        }
    }
}
