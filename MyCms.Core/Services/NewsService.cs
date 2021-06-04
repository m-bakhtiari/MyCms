using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        public NewsService(INewsRepository newsRepository, ISaveChangesRepository saveChangesRepository)
        {
            _newsRepository = newsRepository;
            _saveChangesRepository = saveChangesRepository;
        }

        public async Task<OpRes> AddNewsAsync(NewsViewModel newsViewModel)
        {
            var error = ValidateNews(newsViewModel);
            if (error.IsNullOrWhiteSpace() == false)
            {
                return OpRes.BuildError(error);
            }

            var news = newsViewModel.ToNews();
            var newsImage = await AddProductImage(newsViewModel.Image);
            news.ImageName = newsImage;
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
        private async Task<string> AddProductImage(IFormFile image)
        {
            var imageName = StringExtensions.GenerateUniqGuidCodeWithoutDashes() + Path.GetExtension(image.FileName);
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/ProductImage", imageName);
            await using var stream = new FileStream(imagePath, FileMode.Create);
            await image.CopyToAsync(stream);
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

        public async Task<NewsDto> GetNewsByNewsId(int newsId)
        {
            return await _newsRepository.GetNewsByNewsIdAsync(newsId);
        }

        public async Task<PagedResult<NewsDto, NewsSearchItem>> GetNewsByPaging(NewsSearchItem item)
        {
            return await _newsRepository.GetNewsByPaging(item);
        }
    }
}
