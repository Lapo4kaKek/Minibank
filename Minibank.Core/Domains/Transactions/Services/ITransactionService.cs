using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minibank.Core.Domains.Transactions.Services
{
    public interface ITransaction
    {
        public List<Transaction> GetHistoryPay(string id);
        public void AddTransaction(Transaction transaction);
    }
}
