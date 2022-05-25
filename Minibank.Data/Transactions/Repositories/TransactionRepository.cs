using System;
using System.Collections.Generic;
using Minibank.Core.Domains.Transactions;
using Minibank.Core.Domains.Transactions.Repositories;
namespace Minibank.Data.Transactions.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private static List<TransactionDbModel> _historyTransaction = new List<TransactionDbModel>();
        public List<Transaction> GetHistoryPay(string id)
        {
            var list = new List<Transaction>();
            foreach(var item in _historyTransaction)
            {
                if (item.Id == id)
                {
                    list.Add(new Transaction()
                    { Id = id, Sum = item.Sum, FromAccountId = item.FromAccountId, ToAccountId = item.ToAccountId });
                }
            }
            return list;
        }

        public void AddTransaction(Transaction transaction)
        {
            _historyTransaction.Add(new TransactionDbModel()
            {
                Id = transaction.Id, 
                Sum = transaction.Sum, 
                FromAccountId = transaction.FromAccountId,
                ToAccountId = transaction.ToAccountId });
        }
        public static void AddTransaction(double sum, string fromAccountId, string toAccountId)
        {
            _historyTransaction.Add(new TransactionDbModel()
            {
                Id = Guid.NewGuid().ToString(),
                Sum = sum,
                FromAccountId = fromAccountId,
                ToAccountId = toAccountId,
            });
        }
    }
}
