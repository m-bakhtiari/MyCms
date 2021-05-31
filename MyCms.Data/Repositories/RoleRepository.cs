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

        public async Task AddOrUpdateAsync(Role role)
        {
            if (role.Id == 0)
            {
                await _context.Roles.AddAsync(role);
            }
            else
            {
                var oldRole = await _context.Roles.FindAsync(role.Id);
                if (oldRole != null)
                {
                    oldRole.Title = role.Title;
                }
            }
        }

        public async Task DeleteRoleAsync(int roleId)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(x => x.Id == roleId);
            role.IsDeleted = true;
        }

        public async void Dispose()
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
