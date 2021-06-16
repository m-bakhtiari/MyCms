﻿using Microsoft.Extensions.FileProviders;

namespace MyCms.WebApp.Models
{
    public class NewsViewModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public IFileInfo Image { get; set; }

        public string Tags { get; set; }

        public int CategoryId { get; set; }

        public string ImageName { get; set; }
    }
}
