using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MyCms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpPost]
        //[PermissionChecker]
        public async Task<ActionResult> PostNews([FromForm]IFormFile image)
        {
            return Content("File Saved");
        }

    }
}
