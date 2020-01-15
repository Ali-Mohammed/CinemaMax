using CinemaMax.Helpers;
using CinemaMaxFeeder;
using CinemaMaxFeeder.Database.Model;
using CinemaMaxFeeder.ModelJson;
using GensouSakuya.Aria2.SDK;
using GensouSakuya.Aria2.SDK.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DownloadAgent
{
    public class AddMoviesToTheQueue
    {
        private readonly MovieContext _movieContextDownloadQueue;
        private readonly MovieContext _movieContextUpdate;
        Aria2Client _aria2Client;

        public AddMoviesToTheQueue()
        {
            this._movieContextDownloadQueue = new MovieContext();
            this._movieContextUpdate = new MovieContext();

            this._aria2Client = new Aria2Client(Config.Aria2Url, Config.Aria2Port);
        }

        public async Task AddMovieToTheDownloadQueueAsync()
        {


            Console.WriteLine("Check If there are movies for downloads");
            if (!CheckIfThereAreMoviesInTheQueue())
            {
                Console.WriteLine("THERE IS NO MOVIES IN THE QUEUE");
                return;
            }


            //Check if theres internet connection
            Console.WriteLine("CEHCK THE INTERNET CONNECTION");
            if (!CheckForInternetConnection())
            {
                Console.WriteLine("NO INTERNET CONNECTION");
                return;
            }

            try
            {
                await _aria2Client.TellActive();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ARIA2 is not running");
                return;
            }

            //----------------------------------------------------//
            //Please notice that those functions are run in order!//
            //----------------------------------------------------//


            //Restart the download if the Aria2 Crash or restart
            //this will restart all the download file, and set the status to waiting again and increase the retry
            //[THIS WILL CHECK THE ARIA2]
            Console.WriteLine("CheckIfTheAriaHasBeenRestartAsync");
            await CheckIfTheAriaHasBeenRestartAsync();


            //Change the status of the download file to error when the file errored
            //[THIS WILL CHECK THE ARIA2]
            Console.WriteLine("CheckIfTheDownloadFileIfGetErrored");
            await CheckIfTheDownloadFileIfGetErrored();


            //Restart the error download
            //if for some reason the download file get errored, it will resatrted and increased the retry
            //[THIS WILL CHECK THE DATABASE]
            Console.WriteLine("RestartErrorDownloadFiles");
            //await RestartErrorDownloadFiles();


            //Change the status of the download file to complete when the file finished
            //[THIS WILL CHECK THE ARIA2]
            Console.WriteLine("CheckIfTheDownloadFileComplete");
            await CheckIfTheDownloadFileComplete();




            Console.WriteLine("Total Active Files IN the DATABASE => " + _movieContextDownloadQueue
                .Movies.Where(q => q.DownloadStatus == CinemaMaxFeeder.Database.Model.MovieDownloadStatus.Started)
                .Count());

            Console.WriteLine("Total ERROR Files IN the DATABASE => " + _movieContextDownloadQueue
            .Movies.Where(q => q.DownloadStatus == CinemaMaxFeeder.Database.Model.MovieDownloadStatus.Error)
            .Count());

            Console.WriteLine("Config: Max Downloading Files In the Same Time: " + (Config.MaxDownloadItemsIntheSameTime).ToString());

            if (_movieContextDownloadQueue
                .Movies.Where(q => q.DownloadStatus == CinemaMaxFeeder.Database.Model.MovieDownloadStatus.Started)
                .Count() <= Config.MaxDownloadItemsIntheSameTime - 1)
            {
                Console.WriteLine("AddWaitingMovieAsync");
                await AddWaitingMovieAsync();
                
            }
        }

        private bool CheckIfThereAreMoviesInTheQueue()
        {

            var moviesCount = _movieContextDownloadQueue
                .Movies
                .Where(q => q.DownloadStatus == CinemaMaxFeeder.Database.Model.MovieDownloadStatus.Waiting)
                .Count();

            Console.WriteLine("Total movies are waiting for downloading: " + moviesCount);

            if (moviesCount > 0)
            {
                return true;
            }

            return false;
        }

        private async Task CheckIfTheDownloadFileIfGetErrored()
        {

            var getAllCompleteFiles = await _aria2Client.TellStopped();
            foreach (var downloadFile in getAllCompleteFiles)
            {
                if (downloadFile.Status == "error")
                {
                    Console.WriteLine("----------------------ERROR in FILE IN ARIA2.............................");
                    await MarkDownloadFileAsErrored(downloadFile);
                }
            }
        }

        private async Task MarkDownloadFileAsErrored(DownloadStatusModel downloadFile)
        {

            //Check file if exist into the Database
            if (_movieContextUpdate.Movies
                .Where(Q => Q.DownloadId == downloadFile.GID)
                .Include(In => In.TranscoddedFiles).Count() == 0)
            {
                Console.WriteLine("----------------------FILE NOT FOUND IN THE DATABASE.............................");
                await _aria2Client.Remove(downloadFile.GID);
                await _aria2Client.ForceRemove(downloadFile.GID);

                return;
            }

            //Get the movie instant from the database
            Console.WriteLine("----------------------FILE FOUND IN THE DATABASE.............................");

            var movie = await _movieContextUpdate.Movies
               .Where(Q => Q.DownloadId == downloadFile.GID)
               .Include(In => In.TranscoddedFiles)
               .FirstAsync();

            movie.DownloadStatus = CinemaMaxFeeder.Database.Model.MovieDownloadStatus.Error;

            var getMovieLink = movie.TranscoddedFiles
            .Where(q => q.Resolution == HelperFunctions.FindMovieRes(movie.TranscoddedFiles))
            .First();

            getMovieLink.DownloadStatus = CinemaMaxFeeder.Database.Model.MovieDownloadStatus.Error;
            getMovieLink.FinishDownloadAt = DateTime.Now;


            _movieContextUpdate.SaveChanges();

            await _aria2Client.Remove(downloadFile.GID);
            await _aria2Client.ForceRemove(downloadFile.GID);

        }

        private async Task CheckIfTheDownloadFileComplete()
        {

            var getAllCompleteFiles = await _aria2Client.TellStopped();
            foreach (var downloadFile in getAllCompleteFiles)
            {
                if (downloadFile.Status == "complete")
                {
                    await MarkDownloadFileAsComplete(downloadFile);
                }
            }
        }

        private async Task MarkDownloadFileAsComplete(DownloadStatusModel downloadFile)
        {

            //Check file if exist into the Database
            if (_movieContextUpdate.Movies
                .Where(Q => Q.DownloadId == downloadFile.GID)
                .Include(In => In.TranscoddedFiles).Count() == 0)
            {

                return;
            }

            //Get the movie instant from the database
             var movie = await _movieContextUpdate.Movies
                .Where(Q => Q.DownloadId == downloadFile.GID)
                .Include(In => In.TranscoddedFiles)
                .FirstAsync();

            movie.DownloadStatus = CinemaMaxFeeder.Database.Model.MovieDownloadStatus.Complete;
            movie.FinishDownloadAt = DateTime.Now;

            var getMovieLink = movie.TranscoddedFiles
            .Where(q => q.Resolution == HelperFunctions.FindMovieRes(movie.TranscoddedFiles))
            .First();

            getMovieLink.DownloadStatus = CinemaMaxFeeder.Database.Model.MovieDownloadStatus.Complete;
            getMovieLink.FinishDownloadAt = DateTime.Now;
            getMovieLink.DownloadLocalPath = downloadFile.Files[0].Path;
            getMovieLink.FileSize = downloadFile.TotalLength;


            _movieContextUpdate.SaveChanges();

        }

        private async Task RestartErrorDownloadFiles()
        {
            Console.WriteLine("--------------CHECK THE RestartErrorDownloadFiles in Database Function");
            var getMovies = _movieContextDownloadQueue
                .Movies
                .Where(q => q.DownloadStatus == CinemaMaxFeeder.Database.Model.MovieDownloadStatus.Error)
                .Where(Q => Q.DownloadRetry <= Config.MaxRetryNumber)
                .Include(c => c.TranscoddedFiles).AsQueryable();

            Console.WriteLine("--------------CHECK THE RestartErrorDownloadFiles in Database Function => " + await getMovies.CountAsync());

            foreach (var movie in await getMovies.ToListAsync())
            {
                movie.DownloadStatus = MovieDownloadStatus.Waiting;
                movie.DownloadRetry += 1;

                foreach (var link in movie.TranscoddedFiles)
                {
                    link.DownloadStatus = MovieDownloadStatus.Waiting;
                    link.DownloadRetry += 1;
                }
            }

           await _movieContextDownloadQueue.SaveChangesAsync();
        }

        private async Task CheckIfTheAriaHasBeenRestartAsync()
        {
            bool isRestarted = true;

            var getAllStoppedFiles = await _aria2Client.TellStopped();
            List<string> activeIds = new List<string>();

            Console.WriteLine("Total STOPED FILES in Aria2: " + getAllStoppedFiles.Count);
            foreach (var downloadFile in getAllStoppedFiles)
            {
                isRestarted = false;
            }

            var getAllActiveFiles = await _aria2Client.TellActive();
            Console.WriteLine("Total ACTIVE FILES IN in Aria2: " + getAllActiveFiles.Count);


            foreach (var downloadFile in getAllActiveFiles)
            {
                isRestarted = false;

            }

            if (isRestarted)
            {
                Console.WriteLine("Yes, The server has been restarted!");
                var getMovies = _movieContextDownloadQueue.Movies
                .Where(q => q.DownloadStatus == CinemaMaxFeeder.Database.Model.MovieDownloadStatus.Error || q.DownloadStatus == CinemaMaxFeeder.Database.Model.MovieDownloadStatus.Started)
                .Include(c => c.TranscoddedFiles)
                .ToList();

                foreach (var movie in getMovies)
                {
                    movie.DownloadStatus = MovieDownloadStatus.Waiting;
                    movie.DownloadRetry += 1;

                    foreach (var down in movie.TranscoddedFiles)
                    {
                        down.DownloadStatus = MovieDownloadStatus.Waiting;
                        down.DownloadRetry += 1;

                    }
                }

                _movieContextDownloadQueue.SaveChanges();
            }
        }

        private async Task AddWaitingMovieAsync()
        {
            Console.WriteLine("AddWaitingMovieAsync");
            var getMovies = _movieContextDownloadQueue
                            .Movies
                            .Where(q => q.DownloadStatus == CinemaMaxFeeder.Database.Model.MovieDownloadStatus.Waiting)
                            .OrderByDescending(o => o.Priority).ThenByDescending(o => o.Stars)
                            .Include(c => c.TranscoddedFiles)
                            .Include(c => c.Translations)
                            .First();

            Console.WriteLine("------------------ ADD MOVIE---------------------------");
            Console.WriteLine(getMovies.ArTitle);
            Console.WriteLine(getMovies.Stars);
            Console.WriteLine(getMovies.Priority);
            Console.WriteLine("------------------ ADD MOVIE---------------------------");


            getMovies.DownloadStatus = CinemaMaxFeeder.Database.Model.MovieDownloadStatus.Started;
            await _movieContextDownloadQueue.SaveChangesAsync();



            await AddTheUriToAria2Async(getMovies);
        }

        public static async Task DownloadMediaFilesAsync(Movie movieModel)
        {
            await DownloadFileFromURLAsync(movieModel.ImgObjUrl.ToString(), GetDirPathForSaveMedia(movieModel), movieModel.Img);
            await DownloadFileFromURLAsync(movieModel.ImgThumbObjUrl.ToString(),  GetDirPathForSaveMedia(movieModel), movieModel.ImgThumb);
            await DownloadFileFromURLAsync(movieModel.ImgMediumThumbObjUrl.ToString(),  GetDirPathForSaveMedia(movieModel), movieModel.ImgMediumThumb);


            if (movieModel.IsSlideShow)
            {
                await DownloadFileFromURLAsync(movieModel.ImgBanner.ToString(), GetDirPathForSaveMedia(movieModel), "img-banner.jpg");

            }

            if (movieModel.Translations != null)
            {
                foreach (var trans in movieModel.Translations)
                {
                    var fileName = trans.File.ToString().Split("?")[0].Split("/translation-files/")[1];

                    await DownloadFileFromURLAsync(trans.File.ToString(), GetDirPathForSaveMedia(movieModel), fileName);
                }
            }
        }

        private async Task AddTheUriToAria2Async(Movie getMovies)
        {

            await DownloadMediaFilesAsync(getMovies);


            var getMovieLink = getMovies.TranscoddedFiles
                .Where(q => q.Resolution == HelperFunctions.FindMovieRes(getMovies.TranscoddedFiles))
                .First();

            string saveToDir = GetDirPath(getMovies, getMovieLink);

            var url = getMovieLink.VideoUrl.ToString();

            Console.WriteLine(url);
            var downloadInstant = _aria2Client.AddUri(url, 1, null, 1, saveToDir, 100);
            Console.WriteLine("MOVIE ADDED TO THE QUEUE");
            Console.WriteLine(getMovies.Id);
            Console.WriteLine(getMovies.Nb);
            Console.WriteLine(getMovies.ArTitle);


            foreach (var item in await _aria2Client.TellActive())
            {
                Console.WriteLine(item.Files[0].Path);

            }


            getMovies.DownloadStatus = CinemaMaxFeeder.Database.Model.MovieDownloadStatus.Started;
            getMovieLink.DownloadStatus = CinemaMaxFeeder.Database.Model.MovieDownloadStatus.Started;
            getMovieLink.DownloadRetry = +1;

            getMovieLink.DownloadId = downloadInstant.Result;
            Console.WriteLine("ARIA ID: " + downloadInstant.Result);
            getMovieLink.StartDownloadAt = DateTime.Now;
            getMovies.StartDownloadAt = DateTime.Now;
            getMovies.DownloadRetry = +1;
            getMovies.DownloadId = downloadInstant.Result;


            _movieContextDownloadQueue.SaveChanges();
        }

        private static string GetDirPath(Movie getMovies, TranscoddedFilesJson getMovieLink)
        {
            var typeFolder = "default";

            if (getMovies.Kind == 1)
            {
                typeFolder = "Movies";
            }

            if (getMovies.Kind == 2)
            {
                typeFolder = "Series";
            }

            var saveToDir = Config.BaseFilePath() + typeFolder + "/" + getMovies.Nb.ToString() + "/" + HelperFunctions.Base64Encode(getMovies.EnTitle) + "/" + getMovieLink.Resolution;
            return saveToDir;
        }

        private static string GetDirPathForSaveMedia(Movie getMovies)
        {
            var typeFolder = "default";

            if (getMovies.Kind == 1)
            {
                typeFolder = "Movies";
            }

            if (getMovies.Kind == 2)
            {
                typeFolder = "Series";
            }

            var saveToDir = Config.BaseFilePath() + typeFolder + "/" + getMovies.Nb.ToString() + "/" + HelperFunctions.Base64Encode(getMovies.EnTitle);
            return saveToDir;
        }

        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("https://cinemana.shabakaty.com/whatismyip"))
                    return true;
            }
            catch
            {
                return false;
            }
        }

        public static async Task DownloadFileFromURLAsync(string url, string path, string fileName)
        {
            Console.WriteLine("DOWNLOAD FILE TO: " + path + "/" + fileName);
            Directory.CreateDirectory(path);
            WebClient client = new WebClient();
            await client.DownloadFileTaskAsync(new Uri(url), path + "/" + fileName);
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
