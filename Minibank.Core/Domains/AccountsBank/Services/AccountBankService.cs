using Minibank.Core.Domains.AccountsBank.Repositories;
using Minibank.Core.Domains.Users.Repositories;
using Minibank.Core.Domains.Users.Services;
using System.Collections.Generic;


namespace Minibank.Core.Domains.AccountsBank.Services
{
    public class AccountBankService : IAccountBankService
    {
        private readonly IAccountBankRepository _accountBankRepository;
        //private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        public AccountBankService(IAccountBankRepository accountBankRepository, IUserRepository userRepository)
        {
            _accountBankRepository =accountBankRepository;
            _userRepository = userRepository;
        }
        public void Create(AccountBank accountBank)
        {
            if (!_userRepository.Exists(accountBank.UserId))
            {
                throw new UserFriendlyException("Пользователя с таким id не существует");
            }
            _accountBankRepository.Create(accountBank);
        }


        public AccountBank GetAccount(string id)
        {
            return _accountBankRepository.GetAccount(id);
        }
        public bool IsExists(string id)
        {
            return _accountBankRepository.IsExists(id);
        }
        public void CloseAccount(string id)
        {
            if (IsExists(id))
            {
                var account = GetAccount(id);
                if (account.Balance != 0)
                {
                    throw new UserFriendlyException(message:"На балансе есть деньги, отправь их куда-нибудь...");
                    
                }
            }
            else
            {
                throw new UserFriendlyException("Аккаунта не существует");
            }
            _accountBankRepository.CloseAccount(id);
        }

        public bool IsExistsAccount(string userId)
        {
            return _accountBankRepository.IsExistsAccount(userId);
        }
        public void TransferFee(double sum, string fromAccountBankId, string toAccountBankId, IDatabase database)
        {

            _accountBankRepository.TransferFee(sum, fromAccountBankId, toAccountBankId, database);
        }

        public double Comission(double sum, string fromAccountBankId, string toAccountBankId)
        {
            return _accountBankRepository.Comission(sum, fromAccountBankId, toAccountBankId);
        }
        public IEnumerable<AccountBank> Get(string userId)
        {
            return _accountBankRepository.Get(userId);
        }
    }
}
