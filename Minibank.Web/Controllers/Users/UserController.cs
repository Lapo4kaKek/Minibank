using Microsoft.AspNetCore.Mvc;
using Minibank.Core.Domains.AccountsBank.Services;
using Minibank.Core.Domains.Users;
using Minibank.Core.Domains.Users.Services;
using Minibank.Web.Controllers.Users.Dto;
using System;
using System.Collections.Generic;


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
        public void CreateUser(UserUpdateDto user)
        {
            UserDto model = new UserDto() { Login = user.Login, Email = user.Email };
            _userService.Create(new User
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
        public IEnumerable<User> GetUsers()
        {
            return _userService.GetAll();
        }
        /// <summary>
        /// Update email
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        [HttpPut("{id}/{Login, Email}")]
        public void UpdateUser(string id, UserUpdateDto model)
        {
            _userService.UpdateUser(id, new User() { Email = model.Email, Login = model.Login});
        }
        /// <summary>
        /// Удалить пользователя
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("/{id}")]
        public void DeleteUser(string id)
        {
            _userService.Delete(id);
        }
    }
}
