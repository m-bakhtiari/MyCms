using MongoDB.Driver;
using MyCms.Domain.Entities;
using MyCms.Domain.Interfaces;
using MyCms.Extensions.Consts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace MyCms.Data.Repositories
{
    public class SliderRepository : ISliderRepository
    {
        private readonly IMongoCollection<Slider> _sliderCollection;

        public SliderRepository(IMongoClient mongoClient)
        {
            var db = mongoClient.GetDatabase(Const.MongoDatabaseName);
            _sliderCollection = db.GetCollection<Slider>(Const.SliderCollection);
        }

        public async Task AddSlider(Slider slider)
        {
            await _sliderCollection.InsertOneAsync(slider);
        }

        public async Task DeleteSlider(ObjectId sliderId)
        {
            await _sliderCollection.DeleteOneAsync(s => s.Id == sliderId);
        }

        public async Task<List<Slider>> GetAll()
        {
            var result = await _sliderCollection.FindAsync(FilterDefinition<Slider>.Empty);
            return result.ToList();
        }

        public async Task<Slider> GetSliderById(ObjectId sliderId)
        {
            var result = await _sliderCollection.FindAsync(s => s.Id == sliderId);
            return await result.FirstOrDefaultAsync();
        }

        public async Task UpdateSlider(Slider slider)
        {
            await _sliderCollection.ReplaceOneAsync(s => s.Id == slider.Id, slider);
        }
    }
}
