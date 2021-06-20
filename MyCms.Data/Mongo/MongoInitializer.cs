using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson;
using MongoDB.Driver;
using MyCms.Data.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCms.Data.Mongo
{
    public class MongoInitializer : IDatabaseInitializer
    {
        private bool _initialized;
        private readonly bool _seed;
        private readonly IMongoDatabase _database;
        private readonly IDatabaseSeeder _seeder;

        public MongoInitializer(IMongoDatabase database, IOptions<MongoOptions> options, IDatabaseSeeder seeder)
        {
            _database = database;
            _seed = options.Value.Seed;
            _seeder = seeder;
        }
        public async Task InitializerAsync()
        {
            if (_initialized)
            {
                return;
            }

            RegisterConventions();
            _initialized = true;
            if (!_seed)
            {
                return;
            }

            await _seeder.SeedAsync();
        }

        private void RegisterConventions()
        {
            ConventionRegistry.Register("MyCmsConventions", new MongoConventions(), x => true);
        }
        private class MongoConventions : IConventionPack
        {
            public IEnumerable<IConvention> Conventions => new List<IConvention>
            {
                new IgnoreExtraElementsConvention(true),
                new EnumRepresentationConvention(BsonType.String),
                new CamelCaseElementNameConvention()
            };
        }
    }

}
