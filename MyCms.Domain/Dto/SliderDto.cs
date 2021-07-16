using Microsoft.AspNetCore.Http;
using MongoDB.Bson;

namespace MyCms.Domain.Dto
{
    public class SliderDto
    {
        public ObjectId SliderId { get; set; }
        public IFormFile ImageName { get; set; }
        public int? Position { get; set; }
    }

    public class SliderUpdateDto
    {
        public ObjectId? SliderId { get; set; }
        public int? Position { get; set; }

        public string ImageName { get; set; }

    }
}
