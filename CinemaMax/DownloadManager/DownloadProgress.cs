using CinemaMax.Helpers;
using CinemaMax.Hubs;
using CinemaMax.Models;
using CinemaMaxFeeder;
using GensouSakuya.Aria2.SDK;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaMax.DownloadManager
{
    public class DownloadProgress
    {

        private readonly IHubContext<DownloadProgressHub> _downloadProgressHub;
        private readonly ILogger<DownloadProgress> _logger;
        Aria2Client _aria2Client;

        private MovieContext _movieContext;

        private readonly MovieContext _movieContextReadOnly;

        public DownloadProgress(ILogger<DownloadProgress> logger, IHubContext<DownloadProgressHub> downloadProgressHub)
        {
            _logger = logger;
            this._aria2Client = new Aria2Client("http://127.0.0.1", 6800);
            _logger.LogInformation("ARIA2 " + this._aria2Client);
            _downloadProgressHub = downloadProgressHub;
            this._movieContext = new MovieContext();
            this._movieContextReadOnly = new MovieContext();

        }

        public void StartDownload()
        {
            var timerManager = new TimerManager(UpdateUserWithInfo()
            );
        }

        private Action UpdateUserWithInfo()
        {
            return async () =>
            {


                var downloadsModel = new List<DownloadProgressModel>();

                await UpdateActiveDownload(downloadsModel);
                //await UpdateCompleteOrErrorDownload(downloadsModel);

                await _downloadProgressHub.Clients.All.SendAsync("items", downloadsModel);

            };
        }

        private async Task UpdateCompleteOrErrorDownload(List<DownloadProgressModel> downloadsModel)
        {
            var getAllCompleteFiles = await _aria2Client.TellStopped();
            foreach (var downloadFile in getAllCompleteFiles)
            {

                var downloadsModelItem = new DownloadProgressModel();
                downloadsModelItem.Name = downloadFile.Files[0].Path;

                try
                {
                    var currentDownloadLink = _movieContext.Movies
                    .Where(Q => Q.TranscoddedFiles.Any(C => C.DownloadId == downloadFile.GID))
                    .Include(In => In.TranscoddedFiles)
                    .First();


                    if (currentDownloadLink != null)
                    {
                        if (currentDownloadLink.DownloadStatus == CinemaMaxFeeder.Database.Model.MovieDownloadStatus.Started)
                        {
                            if (downloadFile.Status == "complete")
                            {
                                currentDownloadLink.DownloadStatus = CinemaMaxFeeder.Database.Model.MovieDownloadStatus.Complete;
                                currentDownloadLink.FinishDownloadAt = DateTime.Now;
                                var getMovieLink = currentDownloadLink.TranscoddedFiles.Where(q => q.Resolution == "1080p").First();
                                getMovieLink.DownloadStatus = CinemaMaxFeeder.Database.Model.MovieDownloadStatus.Complete;
                                getMovieLink.FinishDownloadAt = DateTime.Now;
                                _movieContext.SaveChanges();
                            }
                            if (downloadFile.Status == "error")
                            {
                                currentDownloadLink.DownloadStatus = CinemaMaxFeeder.Database.Model.MovieDownloadStatus.Error;
                                var getMovieLink = currentDownloadLink.TranscoddedFiles.Where(q => q.Resolution == "1080p").First();
                                getMovieLink.DownloadStatus = CinemaMaxFeeder.Database.Model.MovieDownloadStatus.Error;
                                _movieContext.SaveChanges();
                                await _aria2Client.Remove(downloadFile.GID);
                            }
                        }
                    }



                    downloadsModelItem.GID = currentDownloadLink.ArTitle;
                    downloadsModelItem.StartDownloadAt = HelperFunctions.GetElapsedTime(currentDownloadLink.StartDownloadAt);
                }
                catch (Exception ex)
                {
                    downloadsModelItem.GID = "Error - Stopped";
                    downloadsModelItem.StartDownloadAt = "Error - Stopped";
                }




                downloadsModelItem.DownloadSpeed = downloadFile.DownloadSpeed;
                downloadsModelItem.DownloadSpeedHuman = HelperFunctions.BytesToString((long)downloadFile.DownloadSpeed);
                downloadsModelItem.CompletedLength = downloadFile.CompletedLength;

                downloadsModelItem.Status = downloadFile.Status;
                downloadsModelItem.Dir = downloadFile.Dir;
                downloadsModelItem.downloadSizeHuman = HelperFunctions.BytesToString((long)downloadFile.TotalLength);
                downloadsModelItem.TotalLength = downloadFile.TotalLength;
                downloadsModelItem.downloadStatus = downloadFile.Status;


                downloadsModelItem.Percent = Math.Round(((double)downloadFile.CompletedLength / (double)downloadFile.TotalLength) * 100, 2);

                if (downloadFile.Status == "error")
                {
                    await _aria2Client.ForceRemove(downloadFile.GID);
                }
                else
                {
                    downloadsModel.Add(downloadsModelItem);
                }


            }
        }

        private async Task UpdateActiveDownload(List<DownloadProgressModel> downloadsModel)
        {
            var getAllDownloadingFiles = await _aria2Client.TellActive();
            foreach (var downloadFile in getAllDownloadingFiles)
            {

                var downloadsModelItem = new DownloadProgressModel();
                downloadsModelItem.Name = downloadFile.Files[0].Path;

                try
                {
                    var currentDownloadLink = _movieContextReadOnly.Movies
                    .Where(Q => Q.TranscoddedFiles.Any(C => C.DownloadId == downloadFile.GID))
                    .Include(In => In.TranscoddedFiles)
                    .First();

                    downloadsModelItem.GID = currentDownloadLink.ArTitle;
                    downloadsModelItem.StartDownloadAt = HelperFunctions.GetElapsedTime(currentDownloadLink.StartDownloadAt);
                }
                catch (Exception ex)
                {
                    downloadsModelItem.GID = "Error";
                    downloadsModelItem.StartDownloadAt = "Error";
                }



                downloadsModelItem.DownloadSpeed = downloadFile.DownloadSpeed;
                downloadsModelItem.DownloadSpeedHuman = HelperFunctions.BytesToString((long)downloadFile.DownloadSpeed);
                downloadsModelItem.CompletedLength = downloadFile.CompletedLength;

                downloadsModelItem.Status = downloadFile.Status;
                downloadsModelItem.Dir = downloadFile.Dir;
                downloadsModelItem.downloadSizeHuman = HelperFunctions.BytesToString((long)downloadFile.TotalLength);
                downloadsModelItem.TotalLength = downloadFile.TotalLength;
                downloadsModelItem.downloadStatus = downloadFile.Status;


                downloadsModelItem.Percent = Math.Round(((double)downloadFile.CompletedLength / (double)downloadFile.TotalLength) * 100, 2);
                downloadsModel.Add(downloadsModelItem);
            }
        }
    }
}
