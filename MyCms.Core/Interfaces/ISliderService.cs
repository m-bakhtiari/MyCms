using MongoDB.Bson;
using MyCms.Core.ViewModels;
using MyCms.Domain.Dto;
using MyCms.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCms.Core.Interfaces
{
    public interface ISliderService
    {
        Task<OpRes> AddSlider(SliderDto slider);
        Task<OpRes> UpdateSlider(SliderUpdateDto sliderDto);
        Task<List<Slider>> GetAll();
        Task<Slider> GetSliderBySliderId(ObjectId sliderId);
        Task<OpRes> DeleteSlider(ObjectId sliderId);
    }
}
