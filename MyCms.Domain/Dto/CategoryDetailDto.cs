using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCms.Domain.Dto
{
    public class CategoryDetailDto
    {
        public int CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public string ImageName { get; set; }
        public int NewsId { get; set; }
        public string NewsTitle { get; set; }
        public DateTime NewsDate { get; set; }

    }

    public class CategoryDetailWithTopFiveNewsDto
    {
        public int CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public ICollection<NewsDto> NewsList { get; set; }
    }
}
