using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaMaxFeeder
{
    public class Config
    {
        public static Enviroment ActiveServer = Enviroment.T30;
        public static bool StartJobsInBackground = false;


        public enum Enviroment { LocalServer, T30 }
        public static string BaseURL = "https://cinemana.shabakaty.com/api/android/";


        public static string SqlServerConnectionStringT30 = @"Data Source = 192.168.1.100,49170;User ID =T30; Password=business##;Database=CinemaMax;TrustServerCertificate=False;";
        public static string SqlServerConnectionStringLocalServer = @"Server=localhost\SQLEXPRESS;Database=CinemaMax;Trusted_Connection=True";

        public static string HangFireSqlServerConnectionStringT30 = "Data Source=192.168.1.100,49170;User ID=T30; Password=business##;Database=CinemaMaxHangFire;TrustServerCertificate=False";
        public static string HangFireSqlServerConnectionStringLocalServer = "Server=localhost\\SQLEXPRESS;Database=CinemaMaxHangFire;Trusted_Connection=True";

        public static string BaseFilePathT30 = @"\\Readynas\s1\";
        public static string BaseFilePathLocalServer = @"D:\CinemaMaxFiles\";

        public static PageNumberType PageNumberMethod = PageNumberType.FromStartToFinish;

        public enum PageNumberType 
        {
            FromStartToFinish,
            ByCountOfTheDatabase
        };


        public static int ItemPerPage = 10;
        public static int MaxRetryNumber = 20;
        public static int MaxDownloadItemsIntheSameTime = 2;
        public static int Aria2Port = 6800;
        public static string Aria2Url = "http://127.0.0.1";



        public static string BaseFilePath()
        {

            if (ActiveServer == Enviroment.LocalServer)
            {
                return BaseFilePathLocalServer;
            }

            if (ActiveServer == Enviroment.T30)
            {
                return BaseFilePathT30;

            }

            return "";
        }


        public static string SQLBaseStringConnection()
        {

            if (ActiveServer == Enviroment.LocalServer)
            {
                return SqlServerConnectionStringLocalServer;
            }

            if (ActiveServer == Enviroment.T30)
            {
                return SqlServerConnectionStringT30;

            }

            return "";
        }

        public static string HangFireBaseStringConnection()
        {

            if (ActiveServer == Enviroment.LocalServer)
            {
                return HangFireSqlServerConnectionStringLocalServer;
            }

            if (ActiveServer == Enviroment.T30)
            {
                return HangFireSqlServerConnectionStringT30;

            }

            return "";
        }


    }
}
