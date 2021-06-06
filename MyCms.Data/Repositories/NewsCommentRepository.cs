using MyCms.Domain.Entities;
using MyCms.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyCms.Data.Context;
using MyCms.Domain.Dto;

namespace MyCms.Data.Repositories
{
    public class NewsCommentRepository : INewsCommentRepository
    {
        private readonly MyCmsContext _context;

        public NewsCommentRepository(MyCmsContext context)
        {
            _context = context;
        }
        public async Task AddAsync(NewsComment comment)
        {
            await _context.NewsComments.AddAsync(comment);
        }

        public async Task<int> CountByNewsId(int newsId)
        {
            return await _context.NewsComments.CountAsync(x => x.NewsId == newsId);
        }

        public async Task DeleteAsync(int commentId)
        {
            var comment = await _context.NewsComments.FindAsync(commentId);
            comment.IsDeleted = true;
            var response = await _context.NewsComments.FirstOrDefaultAsync(x => x.ParentId == commentId);
            if (response != null)
            {
                response.IsDeleted = true;
            }
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<NewsComment> GetCommentByCommentId(int commentId)
        {
            return await _context.NewsComments.FindAsync(commentId);
        }

        public async Task<PagedResult<NewsCommentDto, NewsCommentSearchItem>> GetCommentByNewsId(NewsCommentSearchItem item)
        {
            var res = new PagedResult<NewsCommentDto, NewsCommentSearchItem>() { Items = new List<NewsCommentDto>(), SearchItem = item };
            var comment = _context.NewsComments
                .Include(x => x.Comment).Include(x => x.News).Include(x => x.User)
                .Select(c => new NewsCommentDto()
                {
                    Id = c.Id,
                    Text = c.Text,
                    ParentId = c.ParentId,
                    UserId = c.UserId,
                    Username = c.User.FullName,
                    NewsId = c.NewsId,
                });
            var count = await comment.CountAsync();
            res.CountAll = count;
            res.ItemPerPage = item.ItemPerPage.Value;
            comment = comment.Skip((item.PageId.Value - 1) * item.ItemPerPage.Value).Take(item.ItemPerPage.Value);
            if (item.NewsId != null)
            {
                comment = comment.Where(x => x.NewsId == item.NewsId);
            }
            res.Items = await comment.ToListAsync();
            res.CurrentPage = item.CurrentPage.Value;
            return res;
        }
    }
}
