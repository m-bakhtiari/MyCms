using System;

namespace MyCms.Domain.Dto
{
    public class NewsDto
    {
        /// <summary>
        /// آیدی خبر
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// توضیح مختصر خیز
        /// </summary>
        public string ShortDescription { get; set; }

        /// <summary>
        /// عنوان خبر
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// متن خبر
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// عکس
        /// </summary>
        public string ImageName { get; set; }

        /// <summary>
        /// تگ ها
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// تاریخ ایجاد
        /// </summary>
        public DateTime CreateAt { get; set; }

        /// <summary>
        /// عنوان دسته بندی
        /// </summary>
        public string CategoryTitle { get; set; }

        /// <summary>
        /// آیدی دسته بندی
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// تعداد کامنت ها
        /// </summary>
        public int NewsCommentCount { get; set; }
    }
}
