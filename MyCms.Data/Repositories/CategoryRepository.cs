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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MyCmsContext _context;

        public CategoryRepository(MyCmsContext context)
        {
            _context = context;
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

        public async Task<Category> GetCategoryByCategoryIdAsync(int categoryId)
        {
            return await _context.Categories.FindAsync(categoryId);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
