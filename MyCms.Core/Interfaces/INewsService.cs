using System.Collections.Generic;
using MyCms.Core.ViewModels;
using MyCms.Domain.Dto;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MyCms.Core.Interfaces
{
    public interface INewsService
    {
        #region Fetch Data

        Task<NewsViewModel> GetNewsByNewsId(int newsId);
        Task<PagedResult<NewsDto, NewsSearchItem>> GetNewsByPaging(NewsSearchItem item);

        Task<List<NewsDto>> GetTopTenNewsByComment();

        Task<List<NewsDto>> GetTopFiveFavoriteNews();

        #endregion

        Task<OpRes> AddNewsAsync(NewsViewModel news);
        Task<OpRes> UpdateNewsAsync(NewsViewModel news);
        Task<OpRes> DeleteNewsAsync(int newsId);
        Task<string> AddProductImage(IFormFile image);
        Task DeleteOldImage(IFormFile image);

        #region News Comments

        Task<OpRes> AddCommentAsync(NewsCommentViewModel comment, int userId);
        Task<OpRes> DeleteCommentAsync(int commentId);

        Task<PagedResult<NewsCommentDto, NewsCommentSearchItem>> GetCommentByNewsId(NewsCommentSearchItem item);

        Task<int> CountCommentByNewsId(int newsId);

        #endregion

        #region News Likes

        Task<OpRes> AddOrDeleteNewsLikeAsync(NewsLikeViewModel newsLike, int userId);
        Task<int> NewsLikeCountByNewsId(int newsId);

        #endregion
    }
}
