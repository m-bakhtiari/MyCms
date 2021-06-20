using MyCms.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCms.Domain.Interfaces
{
    public interface ISliderRepository
   {
       Task<List<Slider>> GetAll();
       Task<Slider> GetSliderById(Guid sliderId);
       Task AddSlider(Slider slider);
       Task DeleteSlider(Guid sliderId);
       Task UpdateSlider(Slider slider);
   }
}
