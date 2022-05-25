using Minibank.Core;
using Minibank.Core.Domains.AccountsBank;
using Minibank.Core.Domains.AccountsBank.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Minibank.Data.Transactions.Repositories;
namespace Minibank.Data.AccountsBank
{
    public class AccountBankRepository : IAccountBankRepository
    {
        private static List<AccountBankDbModel> _accountStorage = new List<AccountBankDbModel>();
        public void Create(AccountBank accountBank)
        {
            
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
            var entity = new AccountBankDbModel()
            {
                AccountPriv = accountBank.AccountPriv,
                CloseAccount = accountBank.CloseAccount,
                OpenAccount = accountBank.OpenAccount,
                Balance = accountBank.Balance,
                UserId = accountBank.UserId,
                Currency = accountBank.Currency,
                Id = Guid.NewGuid().ToString()
            };
            _accountStorage.Add(entity);
        }

        public bool IsExistsAccount(string userId)
        {
            //bool result = false;
            foreach (var item in _accountStorage)
            {
                if (item.UserId == userId)
                {
                    return true;
                }
            }
            return false;
        }
        public void CloseAccount(string id)
        {
            foreach (var item in _accountStorage)
            {
                if (item.Id == id)
                {
                    item.CloseAccount = DateTime.Now;
                    break;
                }
            }
        }

        public double Comission(double sum, string fromAccountBankId, string toAccountBankId)
        {
            var fromAccount = _accountStorage.SingleOrDefault(i => i.Id == fromAccountBankId);
            var toAccount = _accountStorage.SingleOrDefault(i => i.Id == toAccountBankId);
            if (fromAccount == null || toAccount == null)
            {
                throw new UserFriendlyException("не валидные id");
            }

            if (fromAccount.UserId == toAccount.UserId)
            {
                return 0;
            }

            if (fromAccount.Balance < sum)
            {
                throw new UserFriendlyException("на счете недостаточно средств", fromAccount.Balance.ToString());
            }
            return double.Parse(String.Format("{0:0.00}", 2 * fromAccount.Balance / 100));
        }

        public AccountBank GetAccount(string id)
        {
            foreach (var item in _accountStorage)
            {
                if (item.Id == id)
                {
                    return (new AccountBank()
                    {
                        Id = item.Id,
                        UserId = item.UserId,
                        Balance = item.Balance,
                        AccountPriv = item.AccountPriv,
                        Currency = item.Currency,
                        OpenAccount = item.OpenAccount,
                        CloseAccount = item.CloseAccount
                    });

                }
            }

            throw new UserFriendlyException(message:"Аккаунта с таким id нет");
        }



        public bool IsExists(string id)
        {
            bool result = false;
            foreach (var item in _accountStorage)
            {
                if (item.Id == id)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }
        public IEnumerable<AccountBank> Get(string userId)
        {
            List<AccountBank> showaccount = new List<AccountBank>();
            foreach (var item in _accountStorage)
            {
                if (item.UserId == userId)
                {
                    showaccount.Add(new AccountBank()
                    {
                        Id = item.Id,
                        UserId = item.UserId,
                        Balance = item.Balance,
                        AccountPriv = item.AccountPriv,
                        Currency = item.Currency,
                        OpenAccount = item.OpenAccount,
                        CloseAccount = item.CloseAccount
                    });
                }
            }
            
            if (showaccount == null) throw new ValidationException("не нашлось");
            return showaccount;
        }

        public void TransferFee(double sum, string fromAccountBankId, string toAccountBankId, IDatabase database)
        {
            var fromAccount = _accountStorage.SingleOrDefault(i => i.Id == fromAccountBankId);
            var toAccount = _accountStorage.SingleOrDefault(i => i.Id == toAccountBankId);
            if (fromAccount == null || toAccount == null)
            {
                throw new UserFriendlyException(message:"неверные данные");
            }
            Calculator a = new Calculator();
            double totalSum = sum + Comission(sum, fromAccountBankId, toAccountBankId);
            if (fromAccount.Balance < totalSum)
            {
                throw new UserFriendlyException(message:"Недостаточно средств", fromAccount.Balance.ToString());
            }
            fromAccount.Balance -= totalSum;
            toAccount.Balance += a.ConvertCurrency(sum, fromAccount.Currency, toAccount.Currency, database);
            TransactionRepository.AddTransaction(sum, fromAccountBankId, toAccountBankId);
        }

        
    }
}
