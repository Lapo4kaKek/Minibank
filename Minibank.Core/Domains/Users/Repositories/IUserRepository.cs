using System.Collections.Generic;

namespace Minibank.Core.Domains.Users.Repositories
{
    public interface IUserRepository
    {
        User Get(string id);
        IEnumerable<User> GetAll();
        void Create(User user);
        void UpdateUser(string login, User user);

        void Delete(string id);

        bool Exists(string UserId);
    }
}
