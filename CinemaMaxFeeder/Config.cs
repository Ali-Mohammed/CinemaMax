using CinemaMaxFeeder.Database.Model;
using CinemaMaxFeeder.ModelJson;
using System.Collections.Generic;
using System.Linq;

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

        public static string BaseFilePathT30 = @"\\192.168.1.150\s1\CinemaMax\Media\";
        public static string BaseFilePathLocalServer = @"D:\CinemaMaxFiles\";

        public static PageNumberType PageNumberMethod = PageNumberType.FromStartToFinish;

        public enum PageNumberType 
        {
            FromStartToFinish,
            ByCountOfTheDatabase
        };


        public static int ItemPerPage = 10;
        public static int MaxRetryNumber = 5;
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

        public static string getDirSaveFile(Movie getMovies)
        {

            var typeFolder = "default";

            if (getMovies.Kind == 1)
            {
                typeFolder = "Movies";
            }

            if (getMovies.Kind == 1)
            {
                typeFolder = "Series";
            }

            var getMovieLink = getMovies.TranscoddedFiles
            .Where(q => q.Resolution == FindMovieRes(getMovies.TranscoddedFiles))
            .First();


            var saveToDir = Config.BaseFilePath() + typeFolder + "/" + getMovies.Nb.ToString() + "/" + Base64Encode(getMovies.EnTitle) + "/" + getMovieLink.Resolution;
            return saveToDir;
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
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
