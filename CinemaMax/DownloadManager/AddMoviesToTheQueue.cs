using CinemaMax.Helpers;
using CinemaMaxFeeder;
using CinemaMaxFeeder.Database.Model;
using CinemaMaxFeeder.ModelJson;
using GensouSakuya.Aria2.SDK;
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
        Aria2Client _aria2Client;

        public AddMoviesToTheQueue()
        {
            this._movieContextDownloadQueue = new MovieContext();
            this._aria2Client = new Aria2Client("http://127.0.0.1", 6800);
        }

        public async Task AddMovieToTheDownloadQueueAsync()
        {

            //Restart the download if the Aria2 Crash or restart
            //this will restart all the download file, and set the status to waiting again and increase the retry
            await CheckIfTheAriaHasBeenRestartAsync();

            //Restart the error download
            //if for some reason the download file get errored, it will resatrted and increased the retry
            await RestartErrorDownloadFiles();


            //Change the status of the download file to complete when the file finished
            await CheckIfTheDownloadFileComplete();


            if (_movieContextDownloadQueue
                .Movies.Where(q => q.DownloadStatus == CinemaMaxFeeder.Database.Model.MovieDownloadStatus.Started)
                .Count() <= Config.MaxDownloadItemsIntheSameTime - 1)
            {
                    AddWaitingMovie();
                
            }
        }

        private async Task CheckIfTheDownloadFileComplete()
        {

            var getAllCompleteFiles = await _aria2Client.TellStopped();
            foreach (var downloadFile in getAllCompleteFiles)
            {

            }
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

        private void AddWaitingMovie()
        {
            var getMovies = _movieContextDownloadQueue
                            .Movies
                            .Where(q => q.DownloadStatus == CinemaMaxFeeder.Database.Model.MovieDownloadStatus.Waiting)
                            .OrderByDescending(o => o.Priority)
                            .Include(c => c.TranscoddedFiles)
                            .First();

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
