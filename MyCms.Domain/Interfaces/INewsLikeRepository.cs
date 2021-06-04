using System;
using MyCms.Domain.Entities;
using System.Threading.Tasks;

namespace MyCms.Domain.Interfaces
{
    public interface INewsLikeRepository:IAsyncDisposable
    {
        Task AddAsync(NewsLike newsLike);
        Task DeleteAsync(int newsId, int userId);
        Task<int> CountByNewsId(int newsId);
    }
}
