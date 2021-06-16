using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using MyCms.Core.Extensions;
using MyCms.Core.Interfaces;
using MyCms.Core.Mapper;
using MyCms.Core.ViewModels;
using MyCms.Domain.Dto;
using MyCms.Domain.Interfaces;
using MyCms.Extensions.Consts;

namespace MyCms.Core.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;
        private readonly ISaveChangesRepository _saveChangesRepository;
        private readonly INewsLikeRepository _newsLikeRepository;
        private readonly INewsCommentRepository _newsCommentRepository;

        public NewsService(INewsRepository newsRepository, ISaveChangesRepository saveChangesRepository, INewsLikeRepository newsLikeRepository, INewsCommentRepository newsCommentRepository)
        {
            _newsRepository = newsRepository;
            _saveChangesRepository = saveChangesRepository;
            _newsLikeRepository = newsLikeRepository;
            _newsCommentRepository = newsCommentRepository;
        }
        public async Task<OpRes> AddNewsAsync(NewsViewModel newsViewModel)
        {
            var error = ValidateNews(newsViewModel);
            if (error.IsNullOrWhiteSpace() == false)
            {
                return OpRes.BuildError(error);
            }

            var news = newsViewModel.ToNews();
            if (newsViewModel.ImageName != null)
            {
                var newsImage = await AddProductImage(newsViewModel.Image);
                news.ImageName = newsImage;
            }
            news.IsDeleted = false;
            news.CreateAt = DateTime.Now;
            await _newsRepository.AddAsync(news);
            await _saveChangesRepository.SaveChangesAsync();
            return OpRes.BuildSuccess();
        }

        public async Task<OpRes> UpdateNewsAsync(NewsViewModel newsViewModel)
        {
            var error = ValidateNews(newsViewModel);
            if (error.IsNullOrWhiteSpace() == false)
            {
                return OpRes.BuildError(error);
            }

            await DeleteOldImage(newsViewModel);
            var news = newsViewModel.ToNews();
            news.Id = newsViewModel.Id.Value;
            _newsRepository.UpdateAsync(news);
            await _saveChangesRepository.SaveChangesAsync();
            return OpRes.BuildSuccess();
        }

        private static string ValidateNews(NewsViewModel news)
        {
            if (news.Title.IsNullOrWhiteSpace())
            {
                return "Title can't be null";
            }
            if (news.Description.IsNullOrWhiteSpace())
            {
                return "Description can't be null";
            }

            if (news.CategoryId == 0)
            {
                return "please choose category title";
            }

            return null;
        }

        /// <summary>
        /// ذخیره عکس داخل پوشه عکس ها
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private async Task<string> AddProductImage(IFileInfo image)
        {
            var imageName = StringExtensions.GenerateUniqGuidCodeWithoutDashes() + Path.GetExtension(image.Name);
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/ProductImage", imageName);
            await using var stream = new FileStream(imagePath, FileMode.Create);
            image.CreateReadStream();
            return imageName;
        }

        private async Task DeleteOldImage(NewsViewModel news)
        {
            var oldNews = await _newsRepository.GetNewsByNewsIdAsync(news.Id.Value);
            if (news.Image != null)
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), Const.NewsImageLocation, oldNews.ImageName);
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
            }
        }

        public async Task<OpRes> DeleteNewsAsync(int newsId)
        {
            var news = await _newsRepository.GetNewsByNewsIdAsync(newsId);
            if (news == null)
            {
                return OpRes.BuildError("News not found");
            }

            await _newsRepository.DeleteNewsAsync(newsId);
            await _saveChangesRepository.SaveChangesAsync();
            return OpRes.BuildSuccess();
        }

        public async Task<NewsViewModel> GetNewsByNewsId(int newsId)
        {
            var res = await _newsRepository.GetNewsByNewsIdAsync(newsId);
            return res.ToNewsViewModel();
        }

        public async Task<PagedResult<NewsDto, NewsSearchItem>> GetNewsByPaging(NewsSearchItem item)
        {
            return await _newsRepository.GetNewsByPaging(item);
        }

        public async Task<OpRes> AddCommentAsync(NewsCommentViewModel comment, int userId)
        {
            if (comment.Text.IsNullOrWhiteSpace())
            {
                return OpRes.BuildError("Text can't be null");
            }

            var newsComment = comment.ToNewsComment(userId);
            await _newsCommentRepository.AddAsync(newsComment);
            await _saveChangesRepository.SaveChangesAsync();
            return OpRes.BuildSuccess();
        }

        public async Task<OpRes> DeleteCommentAsync(int commentId)
        {
            var comment = await _newsCommentRepository.GetCommentByCommentId(commentId);
            if (comment == null)
            {
                return OpRes.BuildError("Comment not found");
            }
            await _newsCommentRepository.DeleteAsync(commentId);
            await _saveChangesRepository.SaveChangesAsync();
            return OpRes.BuildSuccess();
        }

        public async Task<PagedResult<NewsCommentDto, NewsCommentSearchItem>> GetCommentByNewsId(NewsCommentSearchItem item)
        {
            return await _newsCommentRepository.GetCommentByNewsId(item);
        }

        public async Task<OpRes> AddOrDeleteNewsLikeAsync(NewsLikeViewModel newsLike, int userId)
        {
            if (newsLike.NewsId == 0)
            {
                return OpRes.BuildError("News id can't be null");
            }
            var news = await _newsRepository.GetNewsByNewsIdAsync(newsLike.NewsId);
            if (news == null)
            {
                return OpRes.BuildError("News not found");
            }
            var isLiked = await _newsLikeRepository.IsUserLikeNews(newsLike.NewsId, userId);
            if (isLiked)
            {
                await _newsLikeRepository.DeleteAsync(newsLike.NewsId, userId);
            }
            else
            {
                var model = newsLike.ToNewsLike(userId);
                await _newsLikeRepository.AddAsync(model);
            }
            await _saveChangesRepository.SaveChangesAsync();
            return OpRes.BuildSuccess();
        }

        public async Task<int> NewsLikeCountByNewsId(int newsId)
        {
            return await _newsLikeRepository.CountByNewsId(newsId);
        }

        public async Task<List<NewsDto>> GetTopTenNewsByComment()
        {
            return await _newsRepository.GetTopTenNewsByComment();
        }

        public async Task<List<NewsDto>> GetTopFiveFavoriteNews()
        {
            return await _newsRepository.GetTopFiveFavoriteNews();
        }

        public async Task<int> CountCommentByNewsId(int newsId)
        {
            return await _newsCommentRepository.CountByNewsId(newsId);
        }
    }
}
