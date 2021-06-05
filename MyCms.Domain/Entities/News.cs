using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCms.Domain.Entities
{
    public class News
    {
        #region Constructors

        public News()
        {

        }

        #endregion

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(800)]
        public string Title { get; set; }

        [MaxLength(2500)]
        public string ShortDescription { get; set; }

        [Required]
        public string Description { get; set; }

        [MaxLength(500)]
        public string ImageName { get; set; }

        [MaxLength(800)]
        public string Tags { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public DateTime CreateAt { get; set; }

        public bool IsDeleted { get; set; }

        #region Relations

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

        public ICollection<NewsLike> NewsLikes { get; set; }

        public ICollection<NewsComment> NewsComments { get; set; }

        #endregion
    }
}
