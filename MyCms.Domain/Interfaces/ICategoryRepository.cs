using MyCms.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace MyCms.Domain.Interfaces
{
    public interface ICategoryRepository : IAsyncDisposable
    {
        #region Fetch Data

        /// <summary>
        /// Get category data by category id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        Task<Category> GetCategoryByCategoryIdAsync(int categoryId);

        #endregion

        #region Add Or Update

        /// <summary>
        /// add category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        Task AddAsync(Category category);

        /// <summary>
        /// update category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        Task UpdateAsync(Category category);

        #endregion

        #region Remove

        /// <summary>
        /// Change is deleted to true value 
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task DeleteCategoryAsync(int roleId);
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
