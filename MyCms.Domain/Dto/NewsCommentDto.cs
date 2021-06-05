using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCms.Domain.Dto
{
   public class NewsCommentDto
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int UserId { get; set; }

        public int? ParentId { get; set; }

        public string Username { get; set; }
        public int NewsId { get; set; }
    }
}
