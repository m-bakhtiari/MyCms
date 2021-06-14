using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCms.WebApp.Models
{
    public class CategoryViewModel
    {
        /// <summary>
        /// آیدی دسته بندی
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// عنوان دسته بندی
        /// </summary>
        public string Name { get; set; }
    }
}
