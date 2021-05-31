using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCms.Domain.Entities
{
    public class Category
    {
        #region Constructors

        public Category()
        {

        }

        #endregion

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(800)]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        #region Relations

        public ICollection<News> News { get; set; }

        #endregion
    }
}
