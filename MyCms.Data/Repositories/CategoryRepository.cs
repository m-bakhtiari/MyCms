using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyCms.Core.Extensions;
using MyCms.Data.Context;
using MyCms.Domain.Dto;
using MyCms.Domain.Entities;
using MyCms.Domain.Interfaces;

namespace MyCms.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MyCmsContext _context;

        public CategoryRepository(MyCmsContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<CategoryDto, CategorySearchItem>> GetCategoryByPaging(CategorySearchItem item)
        {
            var res = new PagedResult<CategoryDto, CategorySearchItem>() { Items = new List<CategoryDto>(), SearchItem = item };
            var categories = _context.Categories.Include(x => x.News).Select(c => new CategoryDto()
            {
                Id = c.Id,
                Title = c.Name,
                CategoryNewsCount = c.News.Count
            });
            if (item.HasPaging == false)
            {
                res.Items = await categories.ToListAsync();
            }
            else
            {
                var count = await categories.CountAsync();
                res.CountAll = count;
                res.ItemPerPage = item.ItemPerPage.Value;
                if (item.Title.IsNullOrWhiteSpace() == false)
                {
                    categories = categories.Where(x => x.Title.Contains(item.Title));
                }
                categories = categories.Skip((item.PageId.Value - 1) * item.ItemPerPage.Value).Take(item.ItemPerPage.Value);
                res.Items = await categories.ToListAsync();
                res.CurrentPage = item.CurrentPage.Value;
            }

            return res;
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public async Task UpdateAsync(Category category)
        {
            var oldCategory = await _context.Categories.FindAsync(category.Id);
            if (oldCategory != null)
            {
                oldCategory.Name = category.Name;
            }
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            category.IsDeleted = true;
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<CategoryDto> GetCategoryByCategoryIdAsync(int categoryId)
        {
            return await _context.Categories.Select(c => new CategoryDto() { Id = c.Id, Title = c.Name })
                .FirstOrDefaultAsync(x => x.Id == categoryId);
        }

        public async Task<List<CategoryDetailDto>> GetCategoryWithFirstNews()
        {
            return await _context.Categories.Include(x => x.News.OrderByDescending(n=>n.CreateAt))
                .Where(x => x.News.Any()).Select(c => new CategoryDetailDto()
                {
                    NewsId = c.News.FirstOrDefault().Id,
                    CategoryId = c.Id,
                    CategoryTitle = c.Name,
                    ImageName = c.News.FirstOrDefault().ImageName,
                    NewsDate = c.News.FirstOrDefault().CreateAt,
                    NewsTitle = c.News.FirstOrDefault().Title,
                }).ToListAsync();
        }

        public async Task<List<CategoryDetailWithTopFiveNewsDto>> GetCategoryWithTopFiveNews()
        {
            return await _context.Categories.Include(x => x.News.OrderByDescending(n => n.CreateAt).Take(5))
                .Where(x => x.News.Any()).Select(c => new CategoryDetailWithTopFiveNewsDto()
                {
                    NewsList = c.News.Select(a => new NewsDto()
                    {
                        Id = a.Id,
                        Title = a.Title,
                        CreateAt = a.CreateAt,
                        Description = a.Description,
                        ImageName = a.ImageName,
                        ShortDescription = a.ShortDescription,
                        Tags = a.Tags,
                    }).ToList(),
                    CategoryId = c.Id,
                    CategoryTitle = c.Name,
                }).ToListAsync();
        }
    }
}
