using System;
using System.Globalization;

namespace MyCms.WebApp.Extensions
{
    public static class DateConverter
    {
        public static string ToShamsi(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(value) + "/" +
                   pc.GetMonth(value).ToString("00") + "/" +
                   pc.GetDayOfMonth(value).ToString("00");
        }

        public static string ToShamsiWithMonthName(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();
            DateTime date = DateTime.Now;
            string mname = date.GetMonthName();

            return $"{pc.GetDayOfMonth(value).ToString("00")} {date.GetMonthName()} {pc.GetYear(value)}";
        }

        public static DateTime GetDateTimeFromJalaliString(this string jalaliDate)
        {
            PersianCalendar jc = new PersianCalendar();

            try
            {
                string[] date = jalaliDate.Split('/');
                int year = Convert.ToInt32(date[0]);
                int month = Convert.ToInt32(date[1]);
                int day = Convert.ToInt32(date[2]);

                DateTime d = jc.ToDateTime(year, month, day, 0, 0, 0, 0, PersianCalendar.PersianEra);

                return d;
            }
            catch
            {
                throw new FormatException("The input string must be in 0000/00/00 format.");
            }
        }
        public static string GetDayOfWeekName(this DateTime date)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Saturday: return "شنبه";
                case DayOfWeek.Sunday: return "يکشنبه";
                case DayOfWeek.Monday: return "دوشنبه";
                case DayOfWeek.Tuesday: return "سه‏ شنبه";
                case DayOfWeek.Wednesday: return "چهارشنبه";
                case DayOfWeek.Thursday: return "پنجشنبه";
                case DayOfWeek.Friday: return "جمعه";
                default: return "";
            }
        }
        public static string GetMonthName(this DateTime date)
        {
            PersianCalendar jc = new PersianCalendar();
            string pdate = string.Format("{0:0000}/{1:00}/{2:00}", jc.GetYear(date), jc.GetMonth(date), jc.GetDayOfMonth(date));

            string[] dates = pdate.Split('/');
            int month = Convert.ToInt32(dates[1]);

            switch (month)
            {
                case 1: return "فررودين";
                case 2: return "ارديبهشت";
                case 3: return "خرداد";
                case 4: return "تير‏";
                case 5: return "مرداد";
                case 6: return "شهريور";
                case 7: return "مهر";
                case 8: return "آبان";
                case 9: return "آذر";
                case 10: return "دي";
                case 11: return "بهمن";
                case 12: return "اسفند";
                default: return "";
            }

        }
    }
}
