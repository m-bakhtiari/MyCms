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
            var categories = _context.Categories.Select(c => new CategoryDto() { Id = c.Id, Title = c.Name });
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
    }
}
