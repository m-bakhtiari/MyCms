using MyCms.Core.Extensions;
using MyCms.Core.Interfaces;
using MyCms.Core.Mapper;
using MyCms.Core.ViewModels;
using MyCms.Domain.Dto;
using MyCms.Domain.Entities;
using MyCms.Domain.Interfaces;
using System.Threading.Tasks;

namespace MyCms.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISaveChangesRepository _saveChangesRepository;

        public CategoryService(ICategoryRepository categoryRepository, ISaveChangesRepository saveChangesRepository)
        {
            _categoryRepository = categoryRepository;
            _saveChangesRepository = saveChangesRepository;
        }
        public async Task<Category> GetCategoryByCategoryIdAsync(int categoryId)
        {
            return await _categoryRepository.GetCategoryByCategoryIdAsync(categoryId);
        }

        public async Task<OpRes> AddAsync(CategoryViewModel categoryViewModel)
        {
            var error = CategoryValidate(categoryViewModel);
            if (error.IsNullOrWhiteSpace() == false)
            {
                return OpRes.BuildError(error);
            }

            var category = MapViewModelToEntity.ToCategory(categoryViewModel);

            await _categoryRepository.AddAsync(category);
            await _saveChangesRepository.SaveChangesAsync();
            return OpRes.BuildSuccess();
        }

        public async Task<OpRes> UpdateAsync(CategoryViewModel categoryViewModel)
        {
            var error = CategoryValidate(categoryViewModel);
            if (error.IsNullOrWhiteSpace() == false)
            {
                return OpRes.BuildError(error);
            }

            var category = MapViewModelToEntity.ToCategory(categoryViewModel);

            await _categoryRepository.UpdateAsync(category);
            await _saveChangesRepository.SaveChangesAsync();
            return OpRes.BuildSuccess();
        }

        public async Task<OpRes> DeleteCategoryAsync(int categoryId)
        {
            var category = await _categoryRepository.GetCategoryByCategoryIdAsync(categoryId);
            if (category == null)
            {
                return OpRes.BuildError("Not Found");
            }
            await _categoryRepository.DeleteCategoryAsync(categoryId);
            await _saveChangesRepository.SaveChangesAsync();
            return OpRes.BuildSuccess();
        }

        private static string CategoryValidate(CategoryViewModel categoryViewModel)
        {
            if (categoryViewModel.Name.IsNullOrWhiteSpace())
            {
                return "Name can't be null";
            }

            return null;
        }

        public async Task<PagedResult<Category, CategorySearchItem>> GetCategoryByPaging(CategorySearchItem item)
        {
            return await _categoryRepository.GetCategoryByPaging(item);
        }
    }
}
