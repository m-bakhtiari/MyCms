using MyCms.Core.Interfaces;
using MyCms.Core.ViewModels;
using MyCms.Domain.Entities;
using MyCms.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MyCms.Domain.Dto;

namespace MyCms.Core.Services
{
    public class SliderService : ISliderService
    {
        private readonly ISliderRepository _sliderRepository;
        private readonly INewsService _newsService;

        public SliderService(ISliderRepository sliderRepository, INewsService newsService)
        {
            _sliderRepository = sliderRepository;
            _newsService = newsService;
        }

        public async Task<List<Slider>> GetAll()
        {
            return await _sliderRepository.GetAll();
        }

        public async Task<Slider> GetSliderBySliderId(ObjectId sliderId)
        {
            return await _sliderRepository.GetSliderById(sliderId);
        }

        public async Task<OpRes> AddSlider(SliderDto slider)
        {
            var imageName = await _newsService.AddProductImage(slider.ImageName);
            if (slider.Position == null)
            {
                slider.Position = 0;
            }
            var model = new Slider(imageName, slider.Position);
            await _sliderRepository.AddSlider(model);
            return OpRes.BuildSuccess();
        }
        public async Task<OpRes> DeleteSlider(ObjectId sliderId)
        {
            var slider = await _sliderRepository.GetSliderById(sliderId);
            if (slider == null)
            {
                return OpRes.BuildError("موردی یافت نشد");
            }
            await _sliderRepository.DeleteSlider(sliderId);
            return OpRes.BuildSuccess();
        }
        public async Task<OpRes> UpdateSlider(SliderUpdateDto slider)
        {
            var model = await _sliderRepository.GetSliderById(slider.sliderId);
            if (model==null)
            {
                return OpRes.BuildError("موردی یافت نشد");
            }
            model.Position = slider.Position.HasValue ? slider.Position.Value : 0;

            await _sliderRepository.UpdateSlider(model);
            return OpRes.BuildSuccess();
        }
    }
}
