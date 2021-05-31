using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCms.Core.Extensions
{
    public static class StringExtensions
    {
        public static readonly Random rnd = new Random();
        private static Random rng = new Random();

        /// <summary>
        /// Returns true if the string is null or an empty string
        /// </summary>
        /// <param name="content">The string</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string content)
        {
            return string.IsNullOrEmpty(content);
        }

        /// <summary>
        /// این متد تعداد رقم ورودی نوشته ی رندم ایجاد میکند
        /// </summary>
        /// <param name="textNumber"></param>
        /// <returns></returns>
        public static string CreateRandomText(int textNumber)
        {
            // selected characters
            string chars = "2346789ABCDEFGHJKLMNPQRTUVWXYZabcdefghjkmnpqrtuvwxyz";
            // create random generator
            string name;

            // create name
            name = string.Empty;
            while (name.Length < textNumber)
            {
                name += chars.Substring(rnd.Next(chars.Length), 1);
            }
            // add extension
            return name;
        }

        /// <summary>
        /// این متد به تعداد ارقام ورودی یک رقم رندم ایجاد میکند
        /// </summary>
        /// <param name="textNumber"></param>
        /// <returns></returns>
        public static long CreateRandomNumber(int textNumber)
        {
            // selected characters
            string chars = "1234567890";
            // create random generator
            //Random rnd = new Random();
            string name;

            // create name
            name = string.Empty;
            while (name.Length < textNumber)
            {
                name += chars.Substring(StringExtensions.rnd.Next(chars.Length), 1);
            }

            // add extension
            return Convert.ToInt64(name);
        }

        /// <summary>
        /// check for a string is null or empty or empty with spaces
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// این متد اگر نوشته ای نال نباشد آن را تریم میکند
        /// یعنی در هر صورت خطا نمی دهد
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string TryTrim(this string text)
        {
            if (text == null)
            {
                return null;
            }

            return text.Trim();
        }

        /// <summary>
        /// متدی جهت تبدیل ی و ک عربی به معادل فارسی آن ها
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToPersianChars(this string str)
        {
            if (str.IsNullOrEmpty())
            {
                return string.Empty;
            }
            return str.Replace("ك", "ک").Replace("ي", "ی");
        }

        /// <summary>
        /// ترتیب قرار گرفتن اعضای یک لیست را تغییر میده
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<string> Shuffle(List<string> list)
        {
            int n = list.Count;
            while (n > 0)
            {
                n--;
                int k = rng.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }

        /// <summary>
        /// کاراکترهای عربی ک و ی را فارسی کرده و اعداد عربی و فارسی را لاتین میکند.
        /// حذف تمام فاصله ها و اسپیس های وارد شده و تبدیل حروف به شکل کوچک آن
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToCorrectedString(this string str)
        {
            if (str.IsNullOrWhiteSpace())
            {
                return null;
            }

            var perchars = str.ToPersianChars();
            var engnums = perchars.ToEnglishNumber();
            var spaces = engnums.RemoveAllSpaces();
            return engnums;
        }

        /// <summary>
        /// حذف تمام فاصله ها و اسپیس های وارد شده و تبدیل حروف به شکل کوچک آن
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveAllSpaces(this string str)
        {
            if (str.IsNullOrWhiteSpace())
            {
                return null;
            }

            var res = str.Trim().ToLower();
            return res;
        }

        /// <summary>
        /// جنریت کردن کد بکتا و غیرتکراری متنی 
        /// </summary>
        /// <returns></returns>
        public static string GenerateUniqGuidCodeWithoutDashes()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
