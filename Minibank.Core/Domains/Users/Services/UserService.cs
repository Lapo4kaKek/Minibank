using Minibank.Core.Domains.AccountsBank.Repositories;
using Minibank.Core.Domains.AccountsBank.Services;
using Minibank.Core.Domains.Users.Repositories;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;


namespace Minibank.Core.Domains.Users.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountBankRepository _accountBankRepository;
        //private readonly IAccountBankService _accountBankService;
        public UserService() {}
        public UserService(IUserRepository userRepository, IAccountBankRepository accountBankRepository)
        {
            _userRepository = userRepository;
            _accountBankRepository = accountBankRepository;
            //_accountBankService = accountBankService;
        }

        public async Task<User> GetAsync(string id)
        {
            return await _userRepository.GetAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task CreateAsync(User user)
        {
            if (user.Login == null || user.Login.Length > 20)
            {
                // 
                throw new Exception("Не задан логин или длина более 20 символов");
            }
            await _userRepository.CreateAsync(user);
        }

        public async Task UpdateUserAsync(string id, User user)
        {
            await _userRepository.UpdateUserAsync(id, user);
        }
        public async Task DeleteAsync(string id)
        {
            if (await _accountBankRepository.IsExistsAccountAsync(id))
            {
                throw new Exception("У вас есть привязанный аккаунт");
            }

            await _userRepository.DeleteAsync(id);
        }
        public async Task<bool> ExistsAsync(string UserId)
        {
            if (UserId == "" || UserId ==null)
            {
                return false;
            }
            return await _userRepository.ExistsAsync(UserId);
        }
    }
}
