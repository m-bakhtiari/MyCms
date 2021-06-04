using System;

namespace MyCms.Domain.Dto
{
    public class NewsDto
    {
        public int Id { get; set; }
        public string ShortDescription { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public string Tags { get; set; }
        public DateTime CreateAt { get; set; }
        public string CategoryTitle { get; set; }
    }
}
