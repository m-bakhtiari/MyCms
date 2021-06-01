﻿using MyCms.Domain.Dto;
using MyCms.Domain.Entities;
using System;
using System.Collections.Generic;
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

        /// <summary>
        /// get category by paging and search in items 
        /// </summary>
        /// <param name="hasPaging"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<PagedResult<Category, CategorySearchItem>> GetCategoryByPaging(CategorySearchItem item);

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
    }
}
