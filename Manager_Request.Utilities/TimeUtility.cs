using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Manager_Request.Utilities
{
    public static class TimeUtility
    {
        private const string LINUX_TIMEZONE = "Asia/Ho_Chi_Minh";
        private const string WINDOW_TIMEZONE = "SE Asia Standard Time";

        public static DateTime GetTimeZone(DateTime dateTime)
        {
            bool isWindows = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            string timeZoneId = WINDOW_TIMEZONE;
            if (!isWindows)
                timeZoneId = LINUX_TIMEZONE;
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTime, timeZoneId);
        }

        // For from_now and to_now
        private static Dictionary<string, string> relative_time = new Dictionary<string, string>()
    {
        { "future" , "in {0}" },
        { "past" , "in {0}" },
        { "s" , "Khoảng 1 giây trước" },
        { "m" , "Khoảng 1 phút trước" },
        { "mm" , "{0} phút trước" },
        { "h" , "Khoảng một tiếng trước" },
        { "hh" , "{0} giờ trước" },
        { "d" , "Khoảng một ngày trước" },
        { "dd" , "{0} ngày trước" },
        { "M" , "Khoảng tháng trước" },
        { "MM" , "{0} tháng trước" },
        { "Y" , "Khoảng một năm trước" },
        { "YY" , "{0} năm trước" },
        { "-s" , "Vài giây" },
        { "-m" , "Vài phút" },
        { "-mm" , "Trong {0} phút" },
        { "-h" , "Vài giờ" },
        { "-hh" , "Trong {0} giờ" },
        { "-d" , "Khoảng một ngày" },
        { "-dd" , "Trong {0} ngày" },
        { "-M" , "Khoảng một tháng" },
        { "-MM" , "Trong {0} tháng" },
        { "-Y" , "Khoảng vài năm" },
        { "-YY" , "Trong {0} năm" },
    };

        public static string FromNow(this DateTime time_gmt)
        {
            if (time_gmt == null)
            {
                return "invalid";
            }

            var diff = DateTime.Now - time_gmt;
            if (diff.TotalSeconds < 0)
            {
                return time_gmt.ToNow();
            }
            else if (diff.TotalSeconds <= 45)
            {
                return relative_time["s"];
            }
            else if (diff.TotalSeconds <= 90)
            {
                return relative_time["m"];
            }
            else if (diff.TotalMinutes <= 45)
            {
                return string.Format(relative_time["mm"], Convert.ToInt32(diff.TotalMinutes));
            }
            else if (diff.TotalMinutes <= 90)
            {
                return relative_time["h"];
            }
            else if (diff.TotalHours <= 22)
            {
                return string.Format(relative_time["hh"], Convert.ToInt32(diff.TotalHours));
            }
            else if (diff.TotalHours <= 36)
            {
                return relative_time["d"];
            }
            else if (diff.TotalDays <= 25)
            {
                return string.Format(relative_time["dd"], Convert.ToInt32(diff.TotalDays));
            }
            else if (diff.TotalDays <= 45)
            {
                return relative_time["M"];
            }
            else if (diff.TotalDays <= 345)
            {
                return string.Format(relative_time["MM"], Convert.ToInt32(diff.TotalDays / (365.0 / 12.0)));
            }
            else if (diff.TotalDays <= 545)
            {
                return relative_time["Y"];
            }
            else if (diff.TotalDays > 545)
            {
                return string.Format(relative_time["YY"], Convert.ToInt32(diff.TotalDays / 365.0));
            }
            return "invalid";
        }

        public static string ToNow(this DateTime time_gmt)
        {
            if (time_gmt == null)
            {
                return "invalid";
            }

            var diff = time_gmt - DateTime.Now;
            if (diff.TotalSeconds < 0)
            {
                return time_gmt.FromNow();
            }
            else if (diff.TotalSeconds <= 45)
            {
                return relative_time["-s"];
            }
            else if (diff.TotalSeconds <= 90)
            {
                return relative_time["-m"];
            }
            else if (diff.TotalMinutes <= 45)
            {
                return string.Format(relative_time["-mm"], Convert.ToInt32(diff.TotalMinutes));
            }
            else if (diff.TotalMinutes <= 90)
            {
                return relative_time["-h"];
            }
            else if (diff.TotalHours <= 22)
            {
                return string.Format(relative_time["-hh"], Convert.ToInt32(diff.TotalHours));
            }
            else if (diff.TotalHours <= 36)
            {
                return relative_time["-d"];
            }
            else if (diff.TotalDays <= 25)
            {
                return string.Format(relative_time["-dd"], Convert.ToInt32(diff.TotalDays));
            }
            else if (diff.TotalDays <= 45)
            {
                return relative_time["-M"];
            }
            else if (diff.TotalDays <= 345)
            {
                return string.Format(relative_time["-MM"], Convert.ToInt32(diff.TotalDays / (365.0 / 12.0)));
            }
            else if (diff.TotalDays <= 545)
            {
                return relative_time["-Y"];
            }
            else if (diff.TotalDays > 545)
            {
                return string.Format(relative_time["-YY"], Convert.ToInt32(diff.TotalDays / 365.0));
            }
            return "invalid";
        }

        public static int DiffMonth(DateTime d1, DateTime d2)
        {
            return ((d1.Year - d2.Year) * 12) + d1.Month - d2.Month;
        }
    }
}