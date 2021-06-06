using System;
using MyCms.Domain.Entities;
using System.Threading.Tasks;
using MyCms.Domain.Dto;

namespace MyCms.Domain.Interfaces
{
    public interface INewsCommentRepository : IAsyncDisposable
    {
        Task AddAsync(NewsComment comment);
        Task DeleteAsync(int commentId);
        Task<PagedResult<NewsCommentDto, NewsCommentSearchItem>> GetCommentByNewsId(NewsCommentSearchItem item);
        Task<NewsComment> GetCommentByCommentId(int commentId);
        Task<int> CountByNewsId(int newsId);
    }
}
