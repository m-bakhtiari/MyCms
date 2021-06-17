using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCms.Core.Interfaces;
using MyCms.Domain.Dto;
using System.Threading.Tasks;

namespace MyCms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterNewsController : ControllerBase
    {
        #region Constructor

        private readonly INewsService _newsService;

        public FilterNewsController(INewsService newsService)
        {
            _newsService = newsService;
        }
        #endregion

        /// <summary>
        /// گرفتن اخبار با نام دسته بندی آنان با امکان صفحه بندی و سرچ روی اخبار
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(PagedResult<NewsDto, NewsSearchItem>))]
        public async Task<IActionResult> GetNewsByFilter(NewsSearchItem item)
        {
            var res = await _newsService.GetNewsByPaging(item);
            return Ok(res);
        }
    }
}
