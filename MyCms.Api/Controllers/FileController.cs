using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MyCms.Core.Interfaces;
using MyCms.Core.Services;

namespace MyCms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly INewsService _newsService;

        public FileController(INewsService newsService)
        {
            _newsService = newsService;
        }

        /// <summary>
        /// ذخیره عکس در فایل روت در بلیزر
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        [HttpPost]
        [PermissionChecker]
        public async Task<ActionResult> PostNews([FromForm] IFormFile image)
        {
            var res = await _newsService.AddProductImage(image);
            return Ok(res);
        }
    }
}
