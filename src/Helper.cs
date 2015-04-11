using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace YoutubeDL
{
    public static class DateHelper
    {
        public static string ToHumanDate(this DateTime? date)
        {
            if (!date.HasValue) return null;

            DateTime d = date.Value;
            DateTime now = DateTime.Now;
            // 1.
            // Get time span elapsed since the date.
            TimeSpan s = now.Subtract(d);

            // 2.
            // Get total number of days elapsed.
            int dayDiff = (int)s.TotalDays;

            // 3.
            // Get total number of seconds elapsed.
            int secDiff = (int)s.TotalSeconds;

            // 5.
            // Handle same-day times.
            if (dayDiff == 0)
            {
                // A.
                // Less than one minute ago.
                if (secDiff < 60)
                {
                    return "just now";
                }
                // B.
                // Less than 2 minutes ago.
                if (secDiff < 120)
                {
                    return "1 minute ago";
                }
                // C.
                // Less than one hour ago.
                if (secDiff < 3600)
                {
                    return string.Format("{0} minutes ago",
                        Math.Floor((double)secDiff / 60));
                }
                // D.
                // Less than 2 hours ago.
                if (secDiff < 7200)
                {
                    return "1 hour ago";
                }
                // E.
                // Less than one day ago.
                if (secDiff < 86400)
                {
                    return string.Format("{0} hours ago",
                        Math.Floor((double)secDiff / 3600));
                }
            }
            // 6.
            // Handle previous days.
            if (dayDiff == 1)
            {
                return "yesterday";
            }
            if (dayDiff < 7)
            {
                return string.Format("{0} days ago",
                dayDiff);
            }
            if (dayDiff < 31)
            {
                return string.Format("{0} weeks ago",
                Math.Ceiling((double)dayDiff / 7));
            }

            if (now.AddMonths(-2) < d)
                return "a month ago";

            if (now.AddYears(-1) < d)
                return string.Format("{0} months ago", now.Month - d.Month + 12 * (now.Year - d.Year));

            if (now.AddYears(-2) < d)
                return "1 year ago";

            return string.Format("{0} years ago", now.Year - d.Year);
        }
    }
    public class Helper
    {
        public static bool CheckForYoutubeConnection()
        {
            //return true;
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("https://www.youtube.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}