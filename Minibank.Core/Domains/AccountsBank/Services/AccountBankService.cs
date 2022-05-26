using Minibank.Core.Domains.AccountsBank.Repositories;
using Minibank.Core.Domains.Users.Repositories;
using Minibank.Core.Domains.Users.Services;
using System.Collections.Generic;
using System.Threading.Tasks;


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
        public async Task CreateAsync(AccountBank accountBank)
        {
            if (!await _userRepository.ExistsAsync(accountBank.UserId))
            {
                throw new UserFriendlyException("Пользователя с таким id не существует");
            }
            switch (accountBank.Currency)
            {
                case "USD":
                    accountBank.Currency = "USD";
                    break;
                case "EUR":
                    accountBank.Currency = "EUR";
                    break;
                case "RUB":
                    accountBank.Currency = "RUB";
                    break;
                default: throw new UserFriendlyException("Из валют доступны только USD, EUR, RUB");
            }
            await _accountBankRepository.CreateAsync(accountBank);
        }


        public async Task<AccountBank> GetAccountAsync(string id)
        {
            return await _accountBankRepository.GetAccountAsync(id);
        }
        public bool IsExists(string id)
        {
            return _accountBankRepository.IsExistsAccountAsync(id).Result;
        }

        public async Task CloseAccountAsync(string id)
        {
            if (IsExistsAccountAsync(id).Result)
            {
                var account = GetAccountAsync(id);
                if (account.Result.Balance != 0)
                {
                    throw new UserFriendlyException(message:"На балансе есть деньги, отправь их куда-нибудь...");
                    
                }
            }
            else
            {
                throw new UserFriendlyException("Аккаунта не существует");
            }
            await _accountBankRepository.CloseAccountAsync(id);
        }

        public async Task<bool> IsExistsAccountAsync(string userId)
        {
            return await _accountBankRepository.IsExistsAccountAsync(userId);
        }
        public async Task TransferFeeAsync(double sum, string fromAccountBankId, string toAccountBankId, IDatabase database)
        {

            await _accountBankRepository.TransferFeeAsync(sum, fromAccountBankId, toAccountBankId, database);
        }

        public double Comission(double sum, string fromAccountBankId, string toAccountBankId)
        {
            return _accountBankRepository.Comission(sum, fromAccountBankId, toAccountBankId);
        }
        public async Task<IEnumerable<AccountBank>> GetAsync(string userId)
        {
            return await _accountBankRepository.GetAsync(userId);
        }
    }
}
