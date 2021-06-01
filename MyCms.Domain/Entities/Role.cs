using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCms.Domain.Entities
{
    public class Role
    {
        #region Constructor

        public Role()
        {

        }

        #endregion

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [MaxLength(800)]
        public string Title { get; set; }

        public bool IsDeleted { get; set; }

        #region Relations

        public ICollection<UserRole> UserRoles { get; set; }

        #endregion

    }
}
