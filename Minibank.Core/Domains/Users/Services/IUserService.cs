using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minibank.Core.Domains.Users.Services
{
    public interface IUserService
    {
        User Get(string id);
        IEnumerable<User> GetAll();
        void Create(User user);
        void UpdateUser(string id, User user);
        void Delete(string id);
        bool Exists(string UserId);
    }
}
