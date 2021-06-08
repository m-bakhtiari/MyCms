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
    public class TopNewsLikeController : ControllerBase
    {
        #region Constructor

        private readonly INewsService _newsService;

        public TopNewsLikeController(INewsService newsService)
        {
            _newsService = newsService;
        }
        #endregion

        /// <summary>
        /// گرفتن پنج خبر محبوب بر اساس لایک کاربران
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(List<NewsDto>))]
        public async Task<IActionResult> GetTopNewsLike()
        {
            var news = await _newsService.GetTopFiveFavoriteNews();
            return Ok(news);
        }
    }
}
