using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minibank.Core.Domains.AccountsBank
{
    public class AccountBank
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public double Balance { get; set; }
        public string Currency { get; set; }
        public bool AccountPriv { get; set; }
        public DateTime OpenAccount { get; set; }
        public DateTime CloseAccount { get; set; }
    }
}
