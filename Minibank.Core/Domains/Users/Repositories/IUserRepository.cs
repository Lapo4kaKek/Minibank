using System.Collections.Generic;
using System.Threading.Tasks;

namespace Minibank.Core.Domains.Users.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(string id);
        Task<IEnumerable<User>> GetAllAsync();
        Task CreateAsync(User user);
        Task UpdateUserAsync(string login, User user);

        Task DeleteAsync(string id);

        Task<bool> ExistsAsync(string UserId);
    }
}
