using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MyCms.Domain.Entities;
using MyCms.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCms.Data.Repositories
{
    public class SliderRepository : ISliderRepository
    {
        private readonly IMongoDatabase _database;

        public SliderRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task AddSlider(Slider slider) => await Collection.InsertOneAsync(slider);
        public async Task DeleteSlider(Guid sliderId) => await Collection.DeleteOneAsync(x => x.Id == sliderId);

        public async Task<List<Slider>> GetAll() => await Collection.AsQueryable().ToListAsync();


        public async Task<Slider> GetSliderById(Guid sliderId) => await Collection.AsQueryable().FirstOrDefaultAsync(x=>x.Id==sliderId);


        public async Task UpdateSlider(Slider slider)
        {
            throw new NotImplementedException();
        }

        private IMongoCollection<Slider> Collection => _database.GetCollection<Slider>("Sliders");
    }
}
