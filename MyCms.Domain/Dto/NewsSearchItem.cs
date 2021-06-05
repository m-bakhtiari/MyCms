namespace MyCms.Domain.Dto
{
    public class NewsSearchItem : BaseSearchItem
    {
        public string Title { get; set; }
        public int? CategoryId { get; set; }
    }
}
