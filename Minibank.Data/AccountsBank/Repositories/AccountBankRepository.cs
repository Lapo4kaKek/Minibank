using Minibank.Core;
using Minibank.Core.Domains.AccountsBank;
using Minibank.Core.Domains.AccountsBank.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Minibank.Data.Transactions.Repositories;
namespace Minibank.Data.AccountsBank
{
    public class AccountBankRepository : IAccountBankRepository
    {
        private readonly MinibankContext _context;

        public AccountBankRepository(MinibankContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(AccountBank accountBank)
        {
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
            await _context.Accounts.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsExistsAccountAsync(string userId)
        {
            var entity = await _context.Accounts.AsNoTracking()
                .FirstOrDefaultAsync(it => it.UserId == userId);
            if (entity.UserId == null)
            {
                return false;
            }
            return true;
        }

        public async Task CloseAccountAsync(string id)
        {
            await foreach (var item in _context.Accounts)
            {
                if (item.Id == id)
                {
                    item.CloseAccount = DateTime.Now;
                    break;
                }
            }
            _context.SaveChanges();
        }

        public double Comission(double sum, string fromAccountBankId, string toAccountBankId)
        {
            var fromAccount = _context.Accounts.SingleOrDefault(i => i.Id == fromAccountBankId);
            var toAccount = _context.Accounts.SingleOrDefault(i => i.Id == toAccountBankId);
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
            _context.SaveChanges();
            return double.Parse(String.Format("{0:0.00}", 2 * fromAccount.Balance / 100));
        }

        public async Task<AccountBank> GetAccountAsync(string id)
        {
            await foreach (var item in _context.Accounts)
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
        public async Task<IEnumerable<AccountBank>> GetAsync(string userId)
        {
            List<AccountBank> showaccount = new List<AccountBank>();
            await foreach (var item in _context.Accounts)
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

        public async Task TransferFeeAsync(double sum, string fromAccountBankId, string toAccountBankId, IDatabase database)
        {
            var fromAccount = await _context.Accounts.SingleOrDefaultAsync(i => i.Id == fromAccountBankId);
            var toAccount = await _context.Accounts.SingleOrDefaultAsync(i => i.Id == toAccountBankId);
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
            _context.SaveChanges();
        }

        
    }
}
