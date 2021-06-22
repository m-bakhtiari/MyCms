using MyCms.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace MyCms.Domain.Interfaces
{
    public interface ISliderRepository
    {
        Task<List<Slider>> GetAll();
        Task<Slider> GetSliderById(ObjectId sliderId);
        Task AddSlider(Slider slider);
        Task DeleteSlider(ObjectId sliderId);
        Task UpdateSlider(Slider slider);
    }
}
