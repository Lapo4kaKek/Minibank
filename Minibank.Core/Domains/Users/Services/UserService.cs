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

        public UserService(IUserRepository userRepository, IAccountBankRepository accountBankRepository)
        {
            _userRepository = userRepository;
            _accountBankRepository = accountBankRepository;
            //_accountBankService = accountBankService;
        }

        public User Get(string id)
        {
            return _userRepository.Get(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public void Create(User user)
        {
            if (user.Login == null || user.Login.Length > 20)
            {
                // 
                throw new Exception("Не задан логин или длина более 20 символов");
            }
            _userRepository.Create(user);
        }

        public void UpdateUser(string id, User user)
        {
            _userRepository.UpdateUser(id, user);
        }
        public void Delete(string id)
        {
            if (_accountBankRepository.IsExistsAccount(id))
            {
                throw new Exception("У вас есть привязанный аккаунт");
            }

            _userRepository.Delete(id);
        }
        public bool Exists(string UserId)
        {
            return _userRepository.Exists(UserId);
        }
    }
}
