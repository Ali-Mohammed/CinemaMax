using CinemaMaxFeeder.ModelJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaMax.Helpers
{

    public class HelperFunctions
    {
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static String BytesToString(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
        }

        public  static string GetElapsedTime(DateTime datetime)
        {
            TimeSpan ts = DateTime.Now.Subtract(datetime);

            // The trick: make variable contain date and time representing the desired timespan,
            // having +1 in each date component.
            DateTime date = DateTime.MinValue + ts;

            return ProcessPeriod(date.Year - 1, date.Month - 1, "year")
                   ?? ProcessPeriod(date.Month - 1, date.Day - 1, "month")
                   ?? ProcessPeriod(date.Day - 1, date.Hour, "day", "Yesterday")
                   ?? ProcessPeriod(date.Hour, date.Minute, "hour")
                   ?? ProcessPeriod(date.Minute, date.Second, "minute")
                   ?? ProcessPeriod(date.Second, 0, "second")
                   ?? "Right now";
        }

        private static string ProcessPeriod(int value, int subValue, string name, string singularName = null)
        {
            if (value == 0)
            {
                return null;
            }
            if (value == 1)
            {
                if (!String.IsNullOrEmpty(singularName))
                {
                    return singularName;
                }
                string articleSuffix = name[0] == 'h' ? "n" : String.Empty;
                return subValue == 0
                    ? String.Format("A{0} {1} ago", articleSuffix, name)
                    : String.Format("About a{0} {1} ago", articleSuffix, name);
            }
            return subValue == 0
                ? String.Format("{0} {1}s ago", value, name)
                : String.Format("About {0} {1}s ago", value, name);
        }

        public static string FindMovieRes(ICollection<TranscoddedFilesJson> transcoddedFiles)
        {

            foreach (var link in transcoddedFiles)
            {
                if (link.Resolution == "1080p") { return "1080p"; }

            }

            foreach (var link in transcoddedFiles)
            {

                if (link.Resolution == "720p") { return "720p"; }

            }

            foreach (var link in transcoddedFiles)
            {

                if (link.Resolution == "480p") { return "480p"; }

            }

            foreach (var link in transcoddedFiles)
            {

                if (link.Resolution == "360p") { return "360p"; }

            }
            foreach (var link in transcoddedFiles)
            {
                if (link.Resolution == "240p") { return "240p"; }
            }

            return "";
        }
    }
}
