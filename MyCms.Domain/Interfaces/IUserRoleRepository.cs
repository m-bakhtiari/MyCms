using System;
using MyCms.Domain.Entities;
using System.Threading.Tasks;

namespace MyCms.Domain.Interfaces
{
    public interface IUserRoleRepository:IAsyncDisposable
    {
        Task AddUserRoleAsync(UserRole userRole);
        Task DeleteUserRoleAsync(int roleId, int userId);
        Task<bool> IsUserInAdmin(int userId);
    }
}
