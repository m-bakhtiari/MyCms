using System.Collections.Generic;

namespace MyCms.Domain.Dto
{
    public class PagedResult<T, TS> where TS : BaseSearchItem
    {
        public List<T> Items { get; set; }
        public int CountAll { get; set; }
        public int CurrentPage { get; set; }
        public int ItemPerPage { get; set; }
        public TS SearchItem { get; set; }
    }
}
