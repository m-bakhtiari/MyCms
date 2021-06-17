﻿using Microsoft.AspNetCore.Http;

namespace MyCms.Core.ViewModels
{
    public class NewsViewModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public IFormFile Image { get; set; }
        public string ImageName { get; set; }

        public string Tags { get; set; }

        public int CategoryId { get; set; }
    }
}
