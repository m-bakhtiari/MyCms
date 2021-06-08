using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCms.Core.Interfaces;
using System.Threading.Tasks;
using MyCms.Domain.Dto;

namespace MyCms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryNewsController : ControllerBase
    {
        #region Constructor

        private readonly ICategoryService _categoryService;

        public CategoryNewsController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        #endregion


        /// <summary>
        /// گرفتن لیست گروه ها به همراه تازه ترین خبر آنها
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(List<CategoryDetailDto>))]
        public async Task<IActionResult> GetCategoryWithFirstNews()
        {
            var res = await _categoryService.GetCategoryWIthFirstNews();
            return Ok(res);
        }
    }
}
