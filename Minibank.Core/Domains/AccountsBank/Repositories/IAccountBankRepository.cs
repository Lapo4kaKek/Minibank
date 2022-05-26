using System.Collections.Generic;
using System.Threading.Tasks;

namespace Minibank.Core.Domains.AccountsBank.Repositories
{
    public interface IAccountBankRepository
    {
        Task CreateAsync(AccountBank accountBank);
        Task CloseAccountAsync(string id);
        Task<AccountBank> GetAccountAsync(string id);
        Task TransferFeeAsync(double sum, string fromAccountBankId, string toAccountBankId, IDatabase database);
        Task<IEnumerable<AccountBank>> GetAsync(string userId);
        double Comission(double sum, string fromAccountBankId, string toAccountBankId);
        Task<bool> IsExistsAccountAsync(string userId);
    }
}
