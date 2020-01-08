using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaMaxFeeder
{
    public class Config
    {
        public static string BaseURL = "https://cinemana.shabakaty.com/api/android/";
        public static string SqlServerConnectionString = @"Server=localhost\SQLEXPRESS;Database=CinemaMax;Trusted_Connection=True";
        public static int ItemPerPage = 20;
        public static int MaxRetryNumber = 20;
        public static int MaxDownloadItemsIntheSameTime = 2;
        public static string BaseFilePath = @"C:/Users/ali87/Desktop/CinemaMaxFiles/";

    }
}
