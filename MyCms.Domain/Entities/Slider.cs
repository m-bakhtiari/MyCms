using MongoDB.Bson.Serialization.Attributes;
using System;
using MongoDB.Bson;

namespace MyCms.Domain.Entities
{
    public class Slider
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string ImageName { get; set; }
        public int Position { get; set; }

        public Slider()
        {

        }

        public Slider(string imageName, int? position)
        {
            ImageName = imageName;
            Position = position ?? 0;
        }
    }
}
