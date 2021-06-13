using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using MyCms.Core.Interfaces;
using MyCms.Domain.Dto;

namespace MyCms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterCategoryController : ControllerBase
    {
        #region Constructor

        private readonly ICategoryService _categoryService;

        public FilterCategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        #endregion

        /// <summary>
        /// گرفتن عنوان دسته بندی اخبار با قابلیت سرچ روی آنان و صفحه بندی
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(PagedResult<CategoryDto, CategorySearchItem>))]
        public async Task<IActionResult> GetCategory(CategorySearchItem item)
        {
            var res = await _categoryService.GetCategoryByPaging(item);
            return Ok(res);
        }
    }
}
