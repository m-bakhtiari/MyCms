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

        // PUT: api/News/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        //// POST: api/News
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult> PostNews(NewsViewModel newsViewModel)
        //{
        //    var res = await _newsService.AddNewsAsync(newsViewModel);
        //    if (res.IsSuccess == false)
        //    {
        //        return res.ToBadRequestError();
        //    }
        //    return Ok();
        //}

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
