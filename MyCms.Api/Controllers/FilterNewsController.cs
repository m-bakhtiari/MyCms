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
