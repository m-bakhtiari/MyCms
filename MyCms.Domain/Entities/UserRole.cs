using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCms.Domain.Entities
{
    public class UserRole
    {
        #region Constructor

        protected UserRole()
        {

        }

        public UserRole(int userId, int roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }

        #endregion

        [Key]
        public int UserId { get; set; }

        [Key]
        public int RoleId { get; set; }

        public bool IsDeleted { get; set; }


        #region Relations

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [ForeignKey(nameof(RoleId))]
        public Role Role { get; set; }

        #endregion
    }
}
