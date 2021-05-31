using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace MyCms.Core.Extensions
{
    public static class ValidationExtensions
    {
        /// <summary>
        /// سنجش ساختار ایمیل که صحیح وارد شده یا خیر
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool ValidationEmail(string email)
        {
            Regex regex = new Regex(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$");
            if (!regex.IsMatch(email))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// اعتبارسنجی شماره موبایل 
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public static bool ValidateMobileNumber(string mobile)
        {
            if (mobile.IsNullOrWhiteSpace())
            {
                return true;
            }
            if (mobile.StartsWith("09") && mobile.Length == 11)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// استاندارد کردن شماره موبایل
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public static string CorrectMobileNumber(string mobile)
        {
            if (mobile.StartsWith("0098"))
            {
                return mobile.Replace("0098", "0");
            }
            else if (mobile.StartsWith("+98"))
            {
                return mobile.Replace("+98", "0");
            }
            else if (mobile.StartsWith("0") == false && mobile.Length == 10)
            {
                return "0" + mobile;
            }
            else
            {
                return mobile;
            }
        }

        /// <summary>
        /// اعتبارسنجی شماره ثابت 
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public static bool ValidatePhoneNumber(string phoneNumber)
        {
            if (phoneNumber.IsNullOrWhiteSpace())
            {
                return true;
            }
            if (phoneNumber.Length == 8 || phoneNumber.Length == 11)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// صحت عکس وارد شده را تایید می کند تا حتما عکس باشد نه فایل دیگری
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool IsImage(this IFormFile file)
        {
            try
            {
                var img = System.Drawing.Image.FromStream(file.OpenReadStream());
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
