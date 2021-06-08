using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCms.Core.Interfaces;
using MyCms.Core.Services;
using MyCms.Core.ViewModels;
using MyCms.Domain.Dto;
using MyCms.Extensions.Extensions;
using System.Threading.Tasks;
using MyCms.Core.Extensions;

namespace MyCms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [PermissionChecker]
    public class NewsLikeController : ControllerBase
    {
        #region Constructor

        private readonly INewsService _newsService;

        public NewsLikeController(INewsService newsService)
        {
            _newsService = newsService;
        }
        #endregion

        // GET: api/NewsLike/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetNewsLikeCount(int id)
        {
            var newsCount = await _newsService.NewsLikeCountByNewsId(id);
            return Ok(newsCount);
        }

        // POST: api/NewsLike
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostNewsLike(NewsLikeViewModel newsLike)
        {
            var res = await _newsService.AddOrDeleteNewsLikeAsync(newsLike, User.GetUserId());
            if (res.IsSuccess == false)
            {
                return res.ToBadRequestError();
            }
            return Ok();
        }
    }
}
