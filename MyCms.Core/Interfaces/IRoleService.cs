using MyCms.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCms.Core.ViewModels;

namespace MyCms.Core.Interfaces
{
    public interface IRoleService
    {
        #region Fetch Data

        /// <summary>
        /// Get role data by role id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<Role> GetRoleByRoleIdAsync(int roleId);

        #endregion

        #region Add Or Update

        /// <summary>
        /// add or update role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<OpRes> AddOrUpdateAsync(RolesViewModel rolesViewModel);

        #endregion

        #region Remove

        /// <summary>
        /// Change is deleted to true value 
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<OpRes> DeleteRoleAsync(int roleId);
        #endregion
    }
}
