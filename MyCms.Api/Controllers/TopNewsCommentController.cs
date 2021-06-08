using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using MyCms.Core.Interfaces;

namespace MyCms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopNewsCommentController : ControllerBase
    {
        #region Constructor

        private readonly INewsService _newsService;

        public TopNewsCommentController(INewsService newsService)
        {
            _newsService = newsService;
        }
        #endregion

        // GET: api/NewsComment
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetTopNewsComment()
        {
            var res = await _newsService.GetTopTenNewsByComment();
            return Ok(res);
        }
    }
}
