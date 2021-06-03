using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyCms.Data.Context;
using MyCms.Domain.Entities;
using MyCms.Domain.Interfaces;
using MyCms.Extensions.Consts;

namespace MyCms.Data.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly MyCmsContext _context;

        public UserRoleRepository(MyCmsContext context)
        {
            _context = context;
        }

        public async Task AddUserRoleAsync(UserRole userRole)
        {
            await _context.UserRoles.AddAsync(userRole);
        }

        public async Task DeleteUserRoleAsync(int roleId, int userId)
        {
            var userRole = await _context.UserRoles.FirstOrDefaultAsync(x => x.RoleId == roleId && x.UserId == userId);
            userRole.IsDeleted = true;
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<bool> IsUserInAdmin(int userId)
        {
            return await _context.UserRoles.AnyAsync(x => x.RoleId == Const.AdminRoleId && x.UserId == userId);
        }
    }
}
