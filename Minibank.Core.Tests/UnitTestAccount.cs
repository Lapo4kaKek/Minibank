using System;
using System.Threading.Tasks;
using Minibank.Core.Domains.AccountsBank;
using Minibank.Core.Domains.AccountsBank.Services;
using Minibank.Core.Domains.Users;
using Minibank.Core.Domains.Users.Services;
using Xunit;
using Xunit.Sdk;

namespace Minibank.Core.Tests
{
    public class UnitTestAccount
    {
        [Fact]
        public async System.Threading.Tasks.Task CreateOtherCurrency()
        {
            AccountBankService service = new AccountBankService();
            User user = new User {Id = "шмэээээ", Email = "что-то рандомное", Login = "я люблю со..."};
            AccountBank account = new AccountBank
            {
                Id = "123", 
                UserId = user.Id, 
                Currency = "TRASH", 
                Balance = 100, 
                OpenAccount = DateTime.Now,
                CloseAccount = DateTime.MaxValue,
                AccountPriv = true
            };
            var result = await Assert.ThrowsAsync<UserFriendlyException>(() => service.CreateAsync(account));
            Assert.Contains("Из валют доступны только USD, EUR, RUB", 
                result.Message);
        }
        
    }
}