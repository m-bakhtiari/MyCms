using Microsoft.AspNetCore.Mvc;
using MyCms.Core.Interfaces;
using MyCms.Core.Services;
using MyCms.Core.ViewModels;
using MyCms.Domain.Dto;
using MyCms.Extensions.Extensions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace MyCms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        #region Constructor

        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        #endregion

        /// <summary>
        /// گرفتن تمام عنوان دسته بندی اخبار 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(PagedResult<CategoryDto, CategorySearchItem>))]
        public async Task<IActionResult> GetCategory()
        {
            var searchItem = new CategorySearchItem() { HasPaging = false };
            var res = await _categoryService.GetCategoryByPaging(searchItem);
            return Ok(res);
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _categoryService.GetCategoryByCategoryIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // PUT: api/Category/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [PermissionChecker]
        public async Task<IActionResult> PutCategory(int id, CategoryViewModel categoryViewModel)
        {
            if (id != categoryViewModel.Id)
            {
                return BadRequest();
            }

            var res = await _categoryService.UpdateAsync(categoryViewModel);
            if (res.IsSuccess == false)
            {
                return res.ToBadRequestError();
            }
            return Ok();
        }

        /// <summary>
        /// ایجاد دسته بندی خبر جدید
        /// </summary>
        /// <param name="categoryViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionChecker]
        public async Task<ActionResult> PostCategory(CategoryViewModel categoryViewModel)
        {
            var res = await _categoryService.AddAsync(categoryViewModel);
            if (res.IsSuccess == false)
            {
                return res.ToBadRequestError();
            }
            return Ok();
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        [PermissionChecker]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var role = await _categoryService.DeleteCategoryAsync(id);
            if (role.IsSuccess == false)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
