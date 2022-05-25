using System;

namespace Minibank.Web.Controllers.Accounts.Dto
{
    public class AccountDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public double Balance { get; set; }
        public string Currency { get; set; }
        public bool AccountPriv { get; set; }
        public DateTime OpenAccount { get; set; }
        public DateTime CloseAccount { get; set; }

        public AccountDto()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
