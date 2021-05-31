using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCms.Domain.Entities
{
    public class NewsComment
    {
        #region Constructor

        protected NewsComment()
        {

        }

        public NewsComment(int id, string text, int userId, int parentId)
        {
            Id = id;
            Text = text;
            UserId = userId;
            ParentId = parentId;
        } 
        #endregion

        [Key]
        public int Id { get; set; }

        [MaxLength(2500)]
        [Required]
        public string Text { get; set; }

        [Required]
        public int UserId { get; set; }

        public int ParentId { get; set; }

        public bool IsDeleted { get; set; }

        #region Relations

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [ForeignKey(nameof(ParentId))]
        public NewsComment Comment { get; set; }

        #endregion
    }
}
