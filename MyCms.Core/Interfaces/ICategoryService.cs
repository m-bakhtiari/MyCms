using MyCms.Core.ViewModels;
using MyCms.Domain.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        Task<CategoryDto> GetCategoryByCategoryIdAsync(int categoryId);

        /// <summary>
        /// get category by paging and search in items 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<PagedResult<CategoryDto, CategorySearchItem>> GetCategoryByPaging(CategorySearchItem item);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<CategoryDetailDto>> GetCategoryWIthFirstNews();

        Task<List<CategoryDetailWithTopFiveNewsDto>> GetCategoryWithTopFiveNews();


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
