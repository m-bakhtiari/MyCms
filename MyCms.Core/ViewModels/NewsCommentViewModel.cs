using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCms.Core.ViewModels
{
    public class NewsCommentViewModel
    {
        public string Text { get; set; }
        public int? ParentId { get; set; }
    }
}
