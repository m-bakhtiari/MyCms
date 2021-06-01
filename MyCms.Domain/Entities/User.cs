using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCms.Domain.Entities
{
    public class User
    {
        #region Constructor
        public User()
        {

        }
        #endregion

        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Email { get; set; }

        [Required]
        [MaxLength(500)]
        public string Password { get; set; }

        public DateTime CreateAt { get; set; }

        public bool IsDeleted { get; set; }

        #region Relations

        public ICollection<NewsLike> NewsLikes { get; set; }

        public ICollection<NewsComment> NewsComments { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }

        #endregion

    }
}
