using System;
using System.Collections.Generic;

namespace MyCms.Domain.Dto
{
    public class CategoryDetailDto
    {
        /// <summary>
        /// آیدی دسته بندی
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// عنوان دسته بندی
        /// </summary>
        public string CategoryTitle { get; set; }
        
        /// <summary>
        /// عکس خبر
        /// </summary>
        public string ImageName { get; set; }

        /// <summary>
        /// آیدی خبر
        /// </summary>
        public int NewsId { get; set; }

        /// <summary>
        /// عنوان خبر
        /// </summary>
        public string NewsTitle { get; set; }

        /// <summary>
        /// تاریخ ایجاد خبر
        /// </summary>
        public DateTime NewsDate { get; set; }

    }

    public class CategoryDetailWithTopFiveNewsDto
    {
        public int CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public ICollection<NewsDto> NewsList { get; set; }
    }
}
