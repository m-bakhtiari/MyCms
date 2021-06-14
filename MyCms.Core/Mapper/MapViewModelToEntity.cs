using MyCms.Core.ViewModels;
using MyCms.Domain.Entities;
using System;

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

        public static NewsLike ToNewsLike(this NewsLikeViewModel newsLikeViewModel, int userId)
        {
            return new NewsLike()
            {
                NewsId = newsLikeViewModel.NewsId,
                UserId = userId
            };
        }

        public static NewsComment ToNewsComment(this NewsCommentViewModel comment, int userId)
        {
            return new NewsComment()
            {
                CreateAt = DateTime.Now,
                IsDeleted = false,
                NewsId = comment.NewsId,
                ParentId = comment.ParentId,
                UserId = userId,
                Text = comment.Text,
            };
        }
    }
}
