using Microsoft.AspNetCore.Mvc;
using MyCms.Core.Interfaces;
using MyCms.Domain.Dto;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace MyCms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SliderController : ControllerBase
    {
        private readonly ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSliders()
        {
            var res = await _sliderService.GetAll();
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> AddSlider(SliderDto sliderDto)
        {
            await _sliderService.AddSlider(sliderDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSlider(SliderDto sliderDto)
        {
            await _sliderService.UpdateSlider(sliderDto);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSliderById(ObjectId id)
        {
            var res = await _sliderService.GetSliderBySliderId(id);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSliderById(ObjectId id)
        {
            var res = await _sliderService.DeleteSlider(id);
            return Ok();
        }
    }
}
