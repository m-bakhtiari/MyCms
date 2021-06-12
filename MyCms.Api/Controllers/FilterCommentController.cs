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
    public class FilterCommentController : ControllerBase
    {
        #region Constructor

        private readonly INewsService _newsService;

        public FilterCommentController(INewsService newsService)
        {
            _newsService = newsService;
        }
        #endregion

        /// <summary>
        /// گرفتن کامنت ها با امکان سرچ و صفحه بندی
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> FilterByPagingNewsComment(NewsCommentSearchItem item)
        {
            var res = await _newsService.GetCommentByNewsId(item);
            return Ok(res);
        }
    }
}
