using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCms.Core.Extensions
{
    public static class NumberExtensions
    {
        [Description("جدا کردن سه تا سه تای ارقام به منظور نمایش پولی")]
        public static string ToCurrency(this long number)
        {

            if (number != 0)
            {
                var pureNumber = RemoveComma(number.ToString());
                decimal isNumber;
                if (pureNumber.Length > 0 && decimal.TryParse(pureNumber, out isNumber))
                {
                    string currentyText = number.ToString("###,###,###,###,###,###,###");
                    return currentyText;
                }
            }
            else
            {
                return "0";
            }
            return number.ToString();
        }

        [Description("حذف علامت ویرگول یا کاما که برای اعداد پولی نمایش داده می شود")]
        public static string RemoveComma(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return null;
            }

            int i = 0;
            string sm = text.ToString();
            foreach (char c in sm)
            {
                if (c == ',')
                {
                    sm = sm.Remove(i, 1);
                    i--;
                }
                i++;
            }
            return sm;
        }

        [Description("جدا کردن سه تا سه تای ارقام به منظور نمایش پولی")]
        public static string ToCurrency(this decimal number)
        {
            if (number != 0)
            {
                var pureNumber = RemoveComma(number.ToString());
                decimal isNumber;
                if (pureNumber.Length > 0 && decimal.TryParse(pureNumber, out isNumber))
                {
                    string currentyText = number.ToString("###,###,###,###,###,###,###");
                    return currentyText;
                }
            }
            else
            {
                return "0";
            }
            return number.ToString();
        }

        public static string ToCurrency(this decimal? number)
        {
            if (number.HasValue)
            {
                return ToCurrency(number.Value);
            }

            return null;
        }

        /// <summary>
        /// این متد کارکترهای عددی فارسی و عربی موجود را به کاراکترهای انگلیسی تبدیل می کند
        /// و اعداد به صورت انگلیسی نمایش داده می شوند
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToEnglishNumber(this string text)
        {
            //var sb = new StringBuilder(text);

            //sb.Replace("۰", "0");
            //sb.Replace("۱", "1");
            //sb.Replace("۲", "2");
            //sb.Replace("۳", "3");
            //sb.Replace("۴", "4");
            //sb.Replace("۵", "5");
            //sb.Replace("۶", "6");
            //sb.Replace("۷", "7");
            //sb.Replace("۸", "8");
            //sb.Replace("۹", "9");

            //sb.Replace("٠", "0");
            //sb.Replace("١", "1");
            //sb.Replace("٢", "2");
            //sb.Replace("٣", "3");
            //sb.Replace("٤", "4");
            //sb.Replace("٥", "5");
            //sb.Replace("٦", "6");
            //sb.Replace("٧", "7");
            //sb.Replace("٨", "8");
            //sb.Replace("٩", "9");

            //return sb.ToString();

            Dictionary<string, string> LettersDictionary = new Dictionary<string, string>
            {
                ["۰"] = "0",
                ["۱"] = "1",
                ["۲"] = "2",
                ["۳"] = "3",
                ["۴"] = "4",
                ["۵"] = "5",
                ["۶"] = "6",
                ["۷"] = "7",
                ["۸"] = "8",
                ["۹"] = "9",
                ["٠"] = "0",
                ["١"] = "1",
                ["٢"] = "2",
                ["٣"] = "3",
                ["٤"] = "4",
                ["٥"] = "5",
                ["٦"] = "6",
                ["٧"] = "7",
                ["٨"] = "8",
                ["٩"] = "9"
            };
            return LettersDictionary.Aggregate(text, (current, item) =>
                current.Replace(item.Key, item.Value));
        }
    }
}
