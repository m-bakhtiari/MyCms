using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCms.Domain.Dto
{
    public abstract class BaseSearchItem
    {
        public int? ItemPerPage { get; set; } = 10;
        public int? PageId { get; set; } = 1;
        public bool HasPaging { get; set; }
    }
}
