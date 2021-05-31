using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCms.Domain.Entities
{
    public class NewsLike
    {
        #region constructor

        public NewsLike()
        {

        }

        public NewsLike(int userId, int newsId)
        {
            UserId = userId;
            NewsId = newsId;
        }

        #endregion

        [Key]
        public int UserId { get; set; }

        [Key]
        public int NewsId { get; set; }

        #region Relations

        [ForeignKey(nameof(NewsId))]
        public News News { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        #endregion
    }
}
