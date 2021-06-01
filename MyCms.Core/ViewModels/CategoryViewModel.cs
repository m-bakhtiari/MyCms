using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCms.Core.ViewModels
{
    public class CategoryViewModel
    {
        /// <summary>
        /// آیدی دسته بندی
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// عنوان دسته بندی
        /// </summary>
        public string Name { get; set; }
    }
}
