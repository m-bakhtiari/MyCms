namespace MyCms.Domain.Dto
{
    public class CategoryDto
    {
        /// <summary>
        /// id of category
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// title of category
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// count of news for each category
        /// </summary>
        public int CategoryNewsCount { get; set; }
    }
}
