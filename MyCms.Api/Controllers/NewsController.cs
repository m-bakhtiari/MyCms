using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCms.Core.Interfaces;
using MyCms.Core.ViewModels;
using MyCms.Domain.Dto;
using MyCms.Extensions.Extensions;
using System.Threading.Tasks;
using MyCms.Core.Services;

namespace MyCms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        #region Constructor

        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }
        #endregion

        /// <summary>
        /// گرفتن اطلاعات یک خبر
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetNews(int id)
        {
            var news = await _newsService.GetNewsByNewsId(id);

            if (news == null)
            {
                return NotFound();
            }

            return Ok(news);
        }

        /// <summary>
        /// ویرایش خبر
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newsViewModel"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [PermissionChecker]
        public async Task<IActionResult> PutNews(int id, NewsViewModel newsViewModel)
        {
            if (id != newsViewModel.Id)
            {
                return BadRequest();
            }

            var res = await _newsService.UpdateNewsAsync(newsViewModel);
            if (res.IsSuccess == false)
            {
                return res.ToBadRequestError();
            }
            return Ok();
        }

        /// <summary>
        /// اضافه کردن خبر جدید
        /// </summary>
        /// <param name="newsViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        //[PermissionChecker]
        public async Task<ActionResult> PostNews(NewsViewModel newsViewModel)
        {
            var res = await _newsService.AddNewsAsync(newsViewModel);
            if (res.IsSuccess == false)
            {
                return res.ToBadRequestError();
            }
            return Ok();
        }

        // DELETE: api/News/5
        [HttpDelete("{id}")]
        [PermissionChecker]
        public async Task<IActionResult> DeleteNews(int id)
        {
            var role = await _newsService.DeleteNewsAsync(id);
            if (role.IsSuccess == false)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
