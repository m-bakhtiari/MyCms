using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCms.Core.Services
{
    public class LoginViewModel
    {
        /// <summary>
        /// ایمیل
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// رمز عبور
        /// </summary>
        public string Password { get; set; }
    }
}
