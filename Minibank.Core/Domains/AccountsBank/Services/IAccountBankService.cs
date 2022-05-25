using System.Collections.Generic;


namespace Minibank.Core.Domains.AccountsBank.Services
{
    public interface IAccountBankService
    {
        void Create(AccountBank accountBank);
        void CloseAccount(string id);
        // считаем кэш
        void TransferFee(double sum, string fromAccountBankId, string toAccountBankId, IDatabase database);
        // считаем комиссию
        double Comission(double sum, string fromAccountBankId, string toAccountBankId);
        IEnumerable<AccountBank> Get(string userId);
        bool IsExists(string id);
        AccountBank GetAccount(string id);
        bool IsExistsAccount(string userId);
    }
}
