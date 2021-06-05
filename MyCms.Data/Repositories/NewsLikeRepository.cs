using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyCms.Data.Context;
using MyCms.Domain.Entities;
using MyCms.Domain.Interfaces;

namespace MyCms.Data.Repositories
{
    public class NewsLikeRepository : INewsLikeRepository
    {
        private readonly MyCmsContext _context;

        public NewsLikeRepository(MyCmsContext context)
        {
            _context = context;
        }
        public async Task AddAsync(NewsLike newsLike)
        {
            await _context.NewsLikes.AddAsync(newsLike);
        }

        public async Task<int> CountByNewsId(int newsId)
        {
            return await _context.NewsLikes.CountAsync(x => x.NewsId == newsId);
        }

        public async Task DeleteAsync(int newsId, int userId)
        {
            var newsLike = await _context.NewsLikes.FirstOrDefaultAsync(x => x.NewsId == newsId && x.UserId == userId);
            _context.NewsLikes.Remove(newsLike);
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<bool> IsUserLikeNews(int newsId, int userId)
        {
            return await _context.NewsLikes.AnyAsync(x => x.NewsId == newsId && x.UserId == userId);
        }
    }
}
