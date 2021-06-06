using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCms.Core.Interfaces;
using MyCms.Core.ViewModels;
using MyCms.Domain.Dto;
using System.Threading.Tasks;
using MyCms.Core.Extensions;
using MyCms.Core.Services;
using MyCms.Extensions.Extensions;

namespace MyCms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsCommentController : ControllerBase
    {
        #region Constructor

        private readonly INewsService _newsService;

        public NewsCommentController(INewsService newsService)
        {
            _newsService = newsService;
        }
        #endregion

        // GET: api/NewsComment
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetNewsComment(NewsCommentSearchItem item)
        {
            var res = await _newsService.GetCommentByNewsId(item);
            return Ok(res);
        }

        // GET: api/NewsComment
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetNewsCommentCount(int newsId)
        {
            var res = await _newsService.CountCommentByNewsId(newsId);
            return Ok(res);
        }


        // GET: api/NewsComment
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetTopNewsComment()
        {
            var res = await _newsService.GetTopTenNewsByComment();
            return Ok(res);
        }

        // POST: api/News
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> PostNews(NewsCommentViewModel comment)
        {
            var res = await _newsService.AddCommentAsync(comment, User.GetUserId());
            if (res.IsSuccess == false)
            {
                return res.ToBadRequestError();
            }
            return Ok();
        }

        // DELETE: api/NewsComment/5
        [HttpDelete("{id}")]
        [PermissionChecker]
        public async Task<IActionResult> DeleteNews(int id)
        {
            var role = await _newsService.DeleteCommentAsync(id);
            if (role.IsSuccess == false)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
