using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCms.Core.Interfaces;
using MyCms.Domain.Entities;

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
        public async Task<IActionResult> AddSliders()
        {
            await _sliderService.AddSlider(new Slider());
            return Ok();
        }
    }
}
