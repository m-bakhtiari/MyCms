using MyCms.Core.ViewModels;
using MyCms.Domain.Dto;
using System.Threading.Tasks;

namespace MyCms.Core.Interfaces
{
    public interface INewsService
    {
        #region Fetch Data

        Task<NewsDto> GetNewsByNewsId(int newsId);
        Task<PagedResult<NewsDto, NewsSearchItem>> GetNewsByPaging(NewsSearchItem item);

        #endregion

        Task<OpRes> AddNewsAsync(NewsViewModel news);
        Task<OpRes> UpdateNewsAsync(NewsViewModel news);
        Task<OpRes> DeleteNewsAsync(int newsId);
    }
}
