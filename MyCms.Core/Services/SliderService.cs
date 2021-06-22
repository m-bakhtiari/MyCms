using MyCms.Core.Interfaces;
using MyCms.Core.ViewModels;
using MyCms.Domain.Entities;
using MyCms.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace MyCms.Core.Services
{
    public class SliderService : ISliderService
    {
        private readonly ISliderRepository _sliderRepository;

        public SliderService(ISliderRepository sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }

        public async Task<List<Slider>> GetAll()
        {
            return await _sliderRepository.GetAll();
        }

        public async Task<Slider> GetSliderBySliderId(ObjectId sliderId)
        {
            return await _sliderRepository.GetSliderById(sliderId);
        }

        public async Task<OpRes> AddSlider(Slider slider)
        {
            //TODO add image
            //TODO validation not be null for image name
            //TODO add in slider collection

            throw new NotImplementedException();
        }
        public async Task<OpRes> DeleteSlider(ObjectId sliderId)
        {
            throw new NotImplementedException();
        }
        public Task<OpRes> UpdateSlider(Slider slider)
        {
            throw new NotImplementedException();
        }
    }
}
