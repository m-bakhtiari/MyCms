using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCms.Domain.Dto
{
    public abstract class BaseSearchItem
    {
        public int? CurrentPage { get; set; } = 1;
        public int? ItemPerPage { get; set; }
        public int? PageId { get; set; } = 1;
        public bool HasPaging { get; set; }
    }
}
