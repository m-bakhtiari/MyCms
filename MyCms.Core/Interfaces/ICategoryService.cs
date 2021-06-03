using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCms.Core.ViewModels;
using MyCms.Domain.Dto;
using MyCms.Domain.Entities;

namespace MyCms.Core.Interfaces
{
    public interface ICategoryService
    {
        #region Fetch Data

        /// <summary>
        /// Get Category data by Category id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        Task<Category> GetCategoryByCategoryIdAsync(int categoryId);

        /// <summary>
        /// get category by paging and search in items 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<PagedResult<CategoryDto, CategorySearchItem>> GetCategoryByPaging(CategorySearchItem item);

        #endregion

        #region Add Or Update

        /// <summary>
        /// add Category
        /// </summary>
        /// <param name="categoryViewModel"></param>
        /// <returns></returns>
        Task<OpRes> AddAsync(CategoryViewModel categoryViewModel);

        /// <summary>
        /// update Category
        /// </summary>
        /// <param name="categoryViewModel"></param>
        /// <returns></returns>
        Task<OpRes> UpdateAsync(CategoryViewModel categoryViewModel);

        #endregion

        #region Remove

        /// <summary>
        /// Change is deleted to true value 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        Task<OpRes> DeleteCategoryAsync(int categoryId);
        #endregion
    }
}
