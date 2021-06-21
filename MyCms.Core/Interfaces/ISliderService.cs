using MyCms.Core.ViewModels;
using MyCms.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCms.Core.Interfaces
{
    public interface ISliderService
   {
       Task<OpRes> AddSlider(Slider slider);
       Task<OpRes> UpdateSlider(Slider slider);
       Task<List<Slider>> GetAll();
       Task<Slider> GetSliderBySliderId(Guid sliderId);
       Task<OpRes> DeleteSlider(Guid sliderId);
   }
}
