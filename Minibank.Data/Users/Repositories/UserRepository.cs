using Minibank.Core.Domains.Users;
using Minibank.Core.Domains.Users.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Threading.Tasks;


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
        public async Task<User> GetAsync(string id)
        {
            var entity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(it => it.Id == id);

            if (entity == null)
            {
                return null;
            }
            var result = new User
            {
                Id = entity.Id,
                Login = entity.Login,
                Email = entity.Email
            };
            return result;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await Task.FromResult<IEnumerable<User>>(_context.Users.Select(it => new User()
            {
                Id = it.Id,
                Email = it.Email,
                Login = it.Login
            }));
        }

        public async Task CreateAsync(User user)
        {
            var entity = new UserDbModel
            {
                Email = user.Email,
                Id = Guid.NewGuid().ToString(),
                Login = user.Login,
                isActive = false,
                Password = Guid.NewGuid().ToString()
            };

            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
            //return Task.CompletedTask;
        }
        
        public async Task UpdateUserAsync(string id, User user)
        {
            var exsistUser = await _context.Users.FirstOrDefaultAsync(_ => _.Id == id);

            if (exsistUser == null)
            {
                throw new ValidationException("Пользователя с таким id не существует");
            }
            exsistUser.Login = user.Login;
            exsistUser.Email = user.Email;
            await _context.SaveChangesAsync();
        }

       

        public async Task DeleteAsync(string id)
        {
            var entity = await _context.Users.FirstOrDefaultAsync(it => it.Id == id);
            
            if (entity != null)
            {
                _context.Users.Remove(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ValidationException("такого пользователя нет");
            }
        }

        public async Task<bool> ExistsAsync(string UserId)
        {
            bool result = false;
            await foreach (var item in _context.Users)
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
