using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minibank.Core.Domains.Users.Services
{
    public interface IUserService
    {
        Task<User> GetAsync(string id);
        Task<IEnumerable<User>> GetAllAsync();
        Task CreateAsync(User user);
        Task UpdateUserAsync(string id, User user);
        Task DeleteAsync(string id);
        Task<bool> ExistsAsync(string UserId);
    }
}
