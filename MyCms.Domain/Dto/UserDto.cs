﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCms.Domain.Dto
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
