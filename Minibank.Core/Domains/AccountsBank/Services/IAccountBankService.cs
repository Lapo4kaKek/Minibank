using System.Collections.Generic;
using System.Threading.Tasks;


namespace Minibank.Core.Domains.AccountsBank.Services
{
    public interface IAccountBankService
    {
        Task CreateAsync(AccountBank accountBank);
        Task CloseAccountAsync(string id);
        // считаем кэш
        Task TransferFeeAsync(double sum, string fromAccountBankId, string toAccountBankId, IDatabase database);
        // считаем комиссию
        double Comission(double sum, string fromAccountBankId, string toAccountBankId);
        Task<IEnumerable<AccountBank>> GetAsync(string userId);
        Task<AccountBank> GetAccountAsync(string id);
        Task<bool> IsExistsAccountAsync(string userId);
    }
}
