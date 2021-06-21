using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MyCms.Domain.Entities;
using MyCms.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyCms.Extensions.Consts;

namespace MyCms.Data.Repositories
{
    public class SliderRepository : ISliderRepository
    {
        public async Task AddSlider(Slider slider)
        {
            var client = new MongoClient(Const.MongoDatabaseConnection);
            var db = client.GetDatabase("MyCmsSlider");
            var sliderCollection = db.GetCollection<Slider>("Sliders");
            await sliderCollection.InsertOneAsync(slider);
        }

        public Task DeleteSlider(Guid sliderId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Slider>> GetAll()
        {
            var client = new MongoClient(Const.MongoDatabaseConnection);
            var db = client.GetDatabase("MyCmsSlider");
            var slider = db.GetCollection<Slider>("Sliders");
            var result = await slider.FindAsync(FilterDefinition<Slider>.Empty);
            return result.ToList();
        }

        public async Task<Slider> GetSliderById(Guid sliderId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSlider(Slider slider)
        {
            throw new NotImplementedException();
        }
    }
}
