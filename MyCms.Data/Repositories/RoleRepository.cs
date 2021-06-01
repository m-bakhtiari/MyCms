using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyCms.Data.Context;
using MyCms.Domain.Entities;
using MyCms.Domain.Interfaces;

namespace MyCms.Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly MyCmsContext _context;

        public RoleRepository(MyCmsContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
        }

        public async Task UpdateAsync(Role role)
        {
            var oldRole = await _context.Roles.FindAsync(role.Id);
            if (oldRole != null)
            {
                oldRole.Title = role.Title;
            }
        }

        public async Task DeleteRoleAsync(int roleId)
        {
            var role = await _context.Roles.FindAsync(roleId);
            role.IsDeleted = true;
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<Role> GetRoleByRoleIdAsync(int roleId)
        {
            return await _context.Roles.FindAsync(roleId);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
