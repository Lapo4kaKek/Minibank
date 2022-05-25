using System.Collections.Generic;

namespace Minibank.Core.Domains.AccountsBank.Repositories
{
    public interface IAccountBankRepository
    {
        void Create(AccountBank accountBank);
        void CloseAccount(string id);
        void TransferFee(double sum, string fromAccountBankId, string toAccountBankId, IDatabase database);
        IEnumerable<AccountBank> Get(string userId);
        double Comission(double sum, string fromAccountBankId, string toAccountBankId);
        bool IsExists(string id);
        bool IsExistsAccount(string userId);
        AccountBank GetAccount(string id);

    }
}
