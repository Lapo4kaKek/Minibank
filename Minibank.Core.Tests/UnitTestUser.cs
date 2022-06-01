using System;
using Minibank.Core.Domains.Users;
using Minibank.Core.Domains.Users.Services;
using Xunit;
namespace Minibank.Core.Tests
{
    public class UnitTestUser
    {
        [Fact]
        public async System.Threading.Tasks.Task AddLoginwithmore20Symbols()
        {
            UserService service = new UserService();
            User user = new User();
            user.Login = "12345678911112131415fbdfgdfgh";
            user.Id = "aboba";
            user.Email = "msuthiscringe@yandex.ru";
            
            var result = await Assert.ThrowsAsync<Exception>(() => service.CreateAsync(user));
            Assert.Contains("Не задан логин или длина более 20 символов", 
                result.Message);
        }
        [Fact]
        public async System.Threading.Tasks.Task ExistsUser()
        {
            UserService service = new UserService();
            User user = new User();
            var result = await service.ExistsAsync(user.Id);
            Assert.False(result);
        }

        [Fact]
        public async System.Threading.Tasks.Task ExistUserNull()
        {
            UserService service = new UserService();
            var result = await service.ExistsAsync(null);
            Assert.False(result);
        }
        [Fact]
        public async System.Threading.Tasks.Task GetUsersNotNull()
        {
            UserService service = new UserService();
            User user = new User {Email = "шмээээээ", Id = "fnjljfgfgdg", Login = "оаоаоаоаоа"};
            await service.CreateAsync(user);
            //var result = service.GetAllAsync();
            // await Assert.ThrowsAsync<NullReferenceException>(()=> service.GetAllAsync());
        }

        /*[Fact]
        public async System.Threading.Tasks.Task GetUsersNull()
        {
            UserService service = new UserService();
            var result = await service.GetAsync("9295fd09-16d5-4031-9a0d-75a4b07718fe");
            Assert.NotNull(result);
        }*/
    }
}