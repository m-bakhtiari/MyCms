using Microsoft.EntityFrameworkCore;
using MyCms.Core.Extensions;
using MyCms.Data.Context;
using MyCms.Domain.Dto;
using MyCms.Domain.Entities;
using MyCms.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCms.Data.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly MyCmsContext _context;

        public NewsRepository(MyCmsContext context)
        {
            _context = context;
        }

        public async Task AddAsync(News news)
        {
            await _context.News.AddAsync(news);
        }

        public async Task DeleteNewsAsync(int newsId)
        {
            var news = await _context.News.FindAsync(newsId);
            news.IsDeleted = true;
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<NewsDto> GetNewsByNewsIdAsync(int newsId)
        {
            return await _context.News.Include(x => x.Category).Select(x => new NewsDto()
            {
                Id = x.Id,
                Title = x.Title,
                CreateAt = x.CreateAt,
                Description = x.Description,
                ImageName = x.ImageName,
                ShortDescription = x.ShortDescription,
                Tags = x.Tags
            }).FirstOrDefaultAsync(x => x.Id == newsId);
        }

        public async Task<PagedResult<NewsDto, NewsSearchItem>> GetNewsByPaging(NewsSearchItem item)
        {
            var res = new PagedResult<NewsDto, NewsSearchItem>() { Items = new List<NewsDto>(), SearchItem = item };
            var news = _context.News.Include(x => x.Category)
                .OrderByDescending(x => x.CreateAt).Select(x => new NewsDto()
                {
                    Id = x.Id,
                    Title = x.Title,
                    CreateAt = x.CreateAt,
                    Description = x.Description,
                    ImageName = x.ImageName,
                    ShortDescription = x.ShortDescription,
                    Tags = x.Tags,
                    CategoryId = x.CategoryId,
                    CategoryTitle = x.Category.Name,
                });
            if (item.HasPaging == false)
            {
                res.Items = await news.ToListAsync();
            }
            else
            {
                var count = await news.CountAsync();
                res.CountAll = count;
                res.ItemPerPage = item.ItemPerPage.Value;
                if (item.Title.IsNullOrWhiteSpace() == false)
                {
                    news = news.Where(x =>
                        x.Title.Contains(item.Title) || x.Description.Contains(item.Title) ||
                        x.ShortDescription.Contains(item.Title) || x.Tags.Contains(item.Title));
                }

                if (item.CategoryId != null)
                {
                    news = news.Where(x => x.CategoryId == item.CategoryId);
                }
                news = news.Skip((item.PageId.Value - 1) * item.ItemPerPage.Value).Take(item.ItemPerPage.Value);
                res.Items = await news.ToListAsync();
            }

            return res;
        }

        public void UpdateAsync(News news)
        {
            _context.News.Update(news);
        }

        public async Task<List<NewsDto>> GetTopFiveFavoriteNews()
        {
            return await _context.News.OrderByDescending(x => x.NewsLikes.Count).Take(5).Select(n => new NewsDto()
            {
                Id = n.Id,
                Title = n.Title,
                CreateAt = n.CreateAt,
                Description = n.Description,
                ImageName = n.ImageName,
                ShortDescription = n.ShortDescription,
                Tags = n.Tags,
                CategoryId = n.CategoryId,
                CategoryTitle = n.Category.Name,
            }).ToListAsync();
        }

        public async Task<List<NewsDto>> GetTopTenNewsByComment()
        {
            return await _context.News.OrderByDescending(x => x.NewsComments.Count).Take(10).Select(n => new NewsDto()
            {
                Id = n.Id,
                Title = n.Title,
                CreateAt = n.CreateAt,
                Description = n.Description,
                ImageName = n.ImageName,
                ShortDescription = n.ShortDescription,
                Tags = n.Tags,
                CategoryId = n.CategoryId,
                CategoryTitle = n.Category.Name,
            }).ToListAsync();
        }

    }
}
