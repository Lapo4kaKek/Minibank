using Minibank.Core.Domains.Transactions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minibank.Core.Domains.Transactions.Services
{
    public class TransactionService : ITransaction
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public void AddTransaction(Transaction transaction)
        {
            _transactionRepository.AddTransaction(transaction);
        }

        public List<Transaction> GetHistoryPay(string id)
        {
            return _transactionRepository.GetHistoryPay(id);
        }
    }
}
