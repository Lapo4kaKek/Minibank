using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minibank.Web.Controllers.Users.Dto
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }

        public UserDto(){ }

        public UserDto(string login, string email)
        {
            Login = login;
            Email = email;
        }

        public UserDto(string login, string email, string id)
        {
            Id = id;
            Login = login;
            Email = email;
        }
        
    }
}
