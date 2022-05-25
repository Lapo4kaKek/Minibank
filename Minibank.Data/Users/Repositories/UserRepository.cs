using Minibank.Core.Domains.Users;
using Minibank.Core.Domains.Users.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;


namespace Minibank.Data.Users.Repositories
{
    public class UserRepository : IUserRepository
    {
        //private static List<UserDbModel> _userStorage = new List<UserDbModel>();
        private readonly MinibankContext _context;

        public UserRepository(MinibankContext context)
        {
            _context = context;
        }
        public User Get(string id)
        {
            var entity = _context.Users
                .AsNoTracking()
                .FirstOrDefault(it => it.Id == id);

            if (entity == null)
            {
                return null;
            }

            return new User
            {
                Id = entity.Id,

            };
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.Select(it => new User()
            {
                Id = it.Id,
                Email = it.Email,
                Login = it.Login
            });
        }

        public void Create(User user)
        {
            var entity = new UserDbModel
            {
                Email = user.Email,
                Id = Guid.NewGuid().ToString(),
                Login = user.Login,
                isActive = false,
                Password = Guid.NewGuid().ToString()
            };

            _context.Users.Add(entity);
            _context.SaveChanges();
        }
        
        public void UpdateUser(string id, User user)
        {
            var exsistUser = _context.Users.FirstOrDefault(_ => _.Id == id);

            if (exsistUser == null)
            {
                throw new ValidationException("Пользователя с таким id не существует");
            }
            exsistUser.Login = user.Login;
            exsistUser.Email = user.Email;
            _context.SaveChanges();
        }

       

        public void Delete(string id)
        {
            var entity = _context.Users.FirstOrDefault(it => it.Id == id);
            
            if (entity != null)
            {
                _context.Users.Remove(entity);
                _context.SaveChanges();
            }
            else
            {
                throw new ValidationException("такого пользователя нет");
            }
        }

        public bool Exists(string UserId)
        {
            bool result = false;
            foreach (var item in _context.Users)
            {
                if (item.Id == UserId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}
