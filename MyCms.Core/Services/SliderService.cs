using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCms.Core.Interfaces;
using MyCms.Core.ViewModels;
using MyCms.Domain.Entities;
using MyCms.Domain.Interfaces;

namespace MyCms.Core.Services
{
    public class SliderService : ISliderService
    {
        private readonly ISliderRepository _sliderRepository;

        public SliderService(ISliderRepository sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }

        public async Task<OpRes> AddSlider(Slider slider)
        {
            await _sliderRepository.AddSlider(new Slider("Test", 1));
            return OpRes.BuildSuccess();
        }

        public Task<OpRes> DeleteSlider(Guid sliderId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Slider>> GetAll()
        {
            return await _sliderRepository.GetAll();
        }

        public Task<Slider> GetSliderBySliderId(Guid sliderId)
        {
            throw new NotImplementedException();
        }

        public Task<OpRes> UpdateSlider(Slider slider)
        {
            throw new NotImplementedException();
        }
    }
}
