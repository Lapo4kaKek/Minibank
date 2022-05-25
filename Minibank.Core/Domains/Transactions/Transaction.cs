using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minibank.Core.Domains.Transactions
{
    public class Transaction
    {
        public string Id { get; set; }
        public double Sum { get; set; }
        public string FromAccountId { get; set; }
        public string ToAccountId { get; set; }
    }
}
