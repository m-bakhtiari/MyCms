using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCms.Core.ViewModels;
using MyCms.Domain.Entities;

namespace MyCms.Core.Mapper
{
    public static class MapViewModelToEntity
    {
        public static Category ToCategory(this CategoryViewModel categoryViewModel)
        {
            return new Category()
            {
                Id = categoryViewModel.Id,
                Name = categoryViewModel.Name
            };
        }

        public static News ToNews(this NewsViewModel newsViewModel)
        {
            return new News()
            {
                CategoryId = newsViewModel.CategoryId,
                Description = newsViewModel.Description,
                ShortDescription = newsViewModel.ShortDescription,
                Title = newsViewModel.Title,
                Tags = newsViewModel.Tags
            };
        }
    }
}
