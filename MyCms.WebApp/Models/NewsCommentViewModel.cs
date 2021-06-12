using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCms.WebApp.Models
{
    public class NewsCommentViewModel
    {
        public string Text { get; set; }
        public int? ParentId { get; set; }
        public int NewsId { get; set; }
    }
}
