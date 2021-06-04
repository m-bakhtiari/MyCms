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
        Task<PagedResult<NewsCommentDto,BaseSearchItem>> GetCommentByNewsId(int commentId,BaseSearchItem item);
    }
}
