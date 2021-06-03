using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyCms.Data.Context;
using MyCms.Domain.Entities;
using MyCms.Domain.Interfaces;
using System.Threading.Tasks;
using MyCms.Domain.Dto;
using MyCms.Core.Extensions;

namespace MyCms.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        #region Ctor

        private readonly MyCmsContext _context;

        public UserRepository(MyCmsContext context)
        {
            _context = context;
        }

        #endregion

        public async Task<int> AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            return user.UserId;
        }

        public async Task<PagedResult<UserDto, UserSearchItem>> GetUserByPaging(UserSearchItem item)
        {
            var res = new PagedResult<UserDto, UserSearchItem>() { Items = new List<UserDto>(), SearchItem = item };
            IQueryable<User> users = _context.Users;
            if (item.HasPaging == false)
            {
                res.Items = await _context.Users.Select(u => new UserDto()
                {
                    Email = u.Email,
                    CreateAt = u.CreateAt
                }).ToListAsync();
            }
            else
            {
                var count = await users.CountAsync();
                res.CountAll = count;
                res.ItemPerPage = item.ItemPerPage.Value;
                if (item.Email.IsNullOrWhiteSpace() == false)
                {
                    users = users.Where(x => x.Email.Contains(item.Email));
                }
                users = users.Skip((item.PageId.Value - 1) * item.ItemPerPage.Value).Take(item.ItemPerPage.Value);
                res.Items = await users.Select(u => new UserDto()
                {
                    Email = u.Email,
                    CreateAt = u.CreateAt,
                    UserId = u.UserId
                }).ToListAsync();
                res.CurrentPage = item.CurrentPage.Value;
            }

            return res;
        }

        public async Task<User> LoginUser(string email, string password)
        {
            return await _context.Users.Include(x => x.UserRoles).ThenInclude(r => r.Role)
                .FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
        }

        public async Task DeleteUser(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return;
            }
            user.IsDeleted = true;
        }


        public async Task<User> GetUserByUserId(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<bool> IsEmailExist(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email == email);
        }
    }
}
