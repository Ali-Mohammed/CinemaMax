using CinemaMax.Helpers;
using CinemaMaxFeeder;
using CinemaMaxFeeder.Database.Model;
using CinemaMaxFeeder.ModelJson;
using GensouSakuya.Aria2.SDK;
using GensouSakuya.Aria2.SDK.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaMax.DownloadManager
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

            //----------------------------------------------------//
            //Please notice that those functions are run in order!//
            //----------------------------------------------------//


            //Restart the download if the Aria2 Crash or restart
            //this will restart all the download file, and set the status to waiting again and increase the retry
            //[THIS WILL CHECK THE ARIA2]
            await CheckIfTheAriaHasBeenRestartAsync();


            //Change the status of the download file to error when the file errored
            //[THIS WILL CHECK THE ARIA2]
            await CheckIfTheDownloadFileIfGetErrored();


            //Restart the error download
            //if for some reason the download file get errored, it will resatrted and increased the retry
            //[THIS WILL CHECK THE DATABASE]
            await RestartErrorDownloadFiles();


            //Change the status of the download file to complete when the file finished
            //[THIS WILL CHECK THE ARIA2]
            await CheckIfTheDownloadFileComplete();



            if (_movieContextDownloadQueue
                .Movies.Where(q => q.DownloadStatus == CinemaMaxFeeder.Database.Model.MovieDownloadStatus.Started)
                .Count() <= Config.MaxDownloadItemsIntheSameTime - 1)
            {
                  await  AddWaitingMovieAsync();
                
            }
        }


        private async Task CheckIfTheDownloadFileIfGetErrored()
        {

            var getAllCompleteFiles = await _aria2Client.TellStopped();
            foreach (var downloadFile in getAllCompleteFiles)
            {
                if (downloadFile.Status == "error")
                {
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
               await _aria2Client.Remove(downloadFile.GID);
               await _aria2Client.ForceRemove(downloadFile.GID);

                return;
            }

            //Get the movie instant from the database
            var movie = await _movieContextUpdate.Movies
               .Where(Q => Q.DownloadId == downloadFile.GID)
               .Include(In => In.TranscoddedFiles)
               .FirstAsync();

            movie.DownloadStatus = CinemaMaxFeeder.Database.Model.MovieDownloadStatus.Error;
            movie.DownloadId = "";

            var getMovieLink = movie.TranscoddedFiles
            .Where(q => q.Resolution == HelperFunctions.FindMovieRes(movie.TranscoddedFiles))
            .First();

            getMovieLink.DownloadStatus = CinemaMaxFeeder.Database.Model.MovieDownloadStatus.Error;
            getMovieLink.FinishDownloadAt = DateTime.Now;
            getMovieLink.DownloadId = "";


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
            var  getMovies = await _movieContextDownloadQueue
                .Movies
                .Where(q => q.DownloadStatus == CinemaMaxFeeder.Database.Model.MovieDownloadStatus.Error)
                .Where(Q => Q.DownloadRetry >= Config.MaxRetryNumber)
                .Include(c => c.TranscoddedFiles)
                .ToListAsync();

            foreach (var movie in getMovies)
            {
                movie.DownloadStatus = MovieDownloadStatus.Waiting;
                movie.DownloadRetry = movie.DownloadRetry + 1;

                foreach (var link in movie.TranscoddedFiles)
                {
                    link.DownloadStatus = MovieDownloadStatus.Waiting;
                    link.DownloadRetry = link.DownloadRetry + 1;
                }
            }

           await _movieContextDownloadQueue.SaveChangesAsync();
        }

        private async Task CheckIfTheAriaHasBeenRestartAsync()
        {
            bool isRestarted = true;

            var getAllStoppedFiles = await _aria2Client.TellStopped();
            List<string> activeIds = new List<string>();

            foreach (var downloadFile in getAllStoppedFiles)
            {
                isRestarted = false;
            }

            var getAllActiveFiles = await _aria2Client.TellActive();

            foreach (var downloadFile in getAllActiveFiles)
            {
                isRestarted = false;

            }

            if (isRestarted)
            {
                var getMovies = _movieContextDownloadQueue.Movies
                .Where(q => q.DownloadStatus == CinemaMaxFeeder.Database.Model.MovieDownloadStatus.Error || q.DownloadStatus == CinemaMaxFeeder.Database.Model.MovieDownloadStatus.Started)
                .Include(c => c.TranscoddedFiles)
                .ToList();

                foreach (var movie in getMovies)
                {
                    movie.DownloadStatus = MovieDownloadStatus.Waiting;
                    movie.DownloadRetry = movie.DownloadRetry + 1;

                    foreach (var down in movie.TranscoddedFiles)
                    {
                        down.DownloadStatus = MovieDownloadStatus.Waiting;
                        down.DownloadRetry = down.DownloadRetry + 1;

                    }
                }

                _movieContextDownloadQueue.SaveChanges();
            }
        }

        private async Task AddWaitingMovieAsync()
        {
            var getMovies = _movieContextDownloadQueue
                            .Movies
                            .Where(q => q.DownloadStatus == CinemaMaxFeeder.Database.Model.MovieDownloadStatus.Waiting)
                            .OrderByDescending(o => o.Priority)
                            .Include(c => c.TranscoddedFiles)
                            .First();

            getMovies.DownloadStatus = CinemaMaxFeeder.Database.Model.MovieDownloadStatus.Started;
            await _movieContextDownloadQueue.SaveChangesAsync();

            AddTheUriToAria2(getMovies, 1);
        }

        private void AddTheUriToAria2(Movie getMovies, long numberOfTry)
        {

            var getMovieLink = getMovies.TranscoddedFiles
                .Where(q => q.Resolution == HelperFunctions.FindMovieRes(getMovies.TranscoddedFiles))
                .First();


            var saveToDir = Config.BaseFilePath + getMovies.Id.ToString() + "/" + HelperFunctions.Base64Encode(getMovies.EnTitle) + "/" + getMovieLink.Resolution;

            var url = getMovieLink.VideoUrl.ToString();
            var downloadInstant = _aria2Client.AddUri(url, 1, null, 1, saveToDir, 100);
            getMovies.DownloadStatus = CinemaMaxFeeder.Database.Model.MovieDownloadStatus.Started;
            getMovieLink.DownloadStatus = CinemaMaxFeeder.Database.Model.MovieDownloadStatus.Started;
            getMovieLink.DownloadRetry = numberOfTry;

            getMovieLink.DownloadId = downloadInstant.Result;
            getMovieLink.StartDownloadAt = DateTime.Now;
            getMovies.StartDownloadAt = DateTime.Now;
            getMovies.DownloadRetry = numberOfTry;
            getMovies.DownloadId = downloadInstant.Result;


            _movieContextDownloadQueue.SaveChanges();
        }

    }
}
