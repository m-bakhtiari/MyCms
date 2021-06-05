using MyCms.Domain.Dto;
using MyCms.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCms.Domain.Interfaces
{
    public interface INewsRepository : IAsyncDisposable
    {
        #region Fetch Data

        /// <summary>
        /// Get News data by News id
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        Task<NewsDto> GetNewsByNewsIdAsync(int newsId);

        /// <summary>
        /// get News by paging and search in items 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<PagedResult<NewsDto, NewsSearchItem>> GetNewsByPaging(NewsSearchItem item);

        Task<List<NewsDto>> GetTopFiveFavoriteNews();

        Task<List<NewsDto>> GetTopTenNewsByComment();

        #endregion

        #region Add Or Update

        /// <summary>
        /// add News
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        Task AddAsync(News news);

        /// <summary>
        /// update News
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        void UpdateAsync(News news);

        #endregion

        #region Remove

        /// <summary>
        /// Change is deleted to true value 
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        Task DeleteNewsAsync(int newsId);
        #endregion
    }
}