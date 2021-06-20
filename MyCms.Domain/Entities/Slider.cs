using System;

namespace MyCms.Domain.Entities
{
    public class Slider
    {
        public Guid Id { get; set; }
        public string ImageName { get; set; }
        public int Position { get; set; }

        public Slider()
        {

        }

        public Slider(string imageName, int? position)
        {
            Id = Guid.NewGuid();
            ImageName = imageName;
            Position = position ?? 0;
        }
    }
}
