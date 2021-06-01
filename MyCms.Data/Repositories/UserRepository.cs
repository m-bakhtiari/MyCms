using Microsoft.EntityFrameworkCore;
using MyCms.Data.Context;
using MyCms.Domain.Entities;
using MyCms.Domain.Interfaces;
using System.Threading.Tasks;

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

        public async Task<User> LoginUser(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
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
    }
}
