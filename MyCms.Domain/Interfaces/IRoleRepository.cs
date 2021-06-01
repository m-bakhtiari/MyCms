﻿using MyCms.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace MyCms.Domain.Interfaces
{
    public interface IRoleRepository : IAsyncDisposable
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
        /// add role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task AddAsync(Role role);

        /// <summary>
        /// update role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task UpdateAsync(Role role);

        #endregion

        #region Remove

        /// <summary>
        /// Change is deleted to true value 
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task DeleteRoleAsync(int roleId);
        #endregion

        #region Save Changes

        /// <summary>
        /// save changes
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync();

        #endregion

    }
}
