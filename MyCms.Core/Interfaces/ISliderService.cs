using MyCms.Core.ViewModels;
using MyCms.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace MyCms.Core.Interfaces
{
    public interface ISliderService
   {
       Task<OpRes> AddSlider(Slider slider);
       Task<OpRes> UpdateSlider(Slider slider);
       Task<List<Slider>> GetAll();
       Task<Slider> GetSliderBySliderId(ObjectId sliderId);
       Task<OpRes> DeleteSlider(ObjectId sliderId);
   }
}
