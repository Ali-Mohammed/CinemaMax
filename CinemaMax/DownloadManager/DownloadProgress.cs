using CinemaMax.Helpers;
using CinemaMax.Hubs;
using CinemaMax.Models;
using CinemaMaxFeeder;
using GensouSakuya.Aria2.SDK;
using GensouSakuya.Aria2.SDK.Model;
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
            this._aria2Client = new Aria2Client(Config.Aria2Url, Config.Aria2Port);
            _logger.LogInformation("ARIA2 " + this._aria2Client);
            _downloadProgressHub = downloadProgressHub;
            this._movieContext = new MovieContext();
            this._movieContextReadOnly = new MovieContext();

        }

        public void StartDownload()
        {
            new TimerManager(UpdateUserWithInfo());
        }

        private Action UpdateUserWithInfo()
        {
            return async () =>
            {


                var downloadsModel = new List<DownloadProgressModel>();

                try {
                    foreach (var downloadFile in await _aria2Client.TellActive())
                    {
                        var downloadsModelItem = new DownloadProgressModel();
                        downloadsModelItem.Name = downloadFile.Files[0].Path;

                        try
                        {
                            var currentDownloadLink = _movieContextReadOnly.Movies
                            .Where(Q => Q.DownloadId == downloadFile.GID)
                            .Include(In => In.TranscoddedFiles)
                            .First();

                            downloadsModelItem.GID = currentDownloadLink.ArTitle;
                            downloadsModelItem.logo = currentDownloadLink.ImgThumbObjUrl;

                            downloadsModelItem.StartDownloadAt = HelperFunctions.GetElapsedTime(currentDownloadLink.StartDownloadAt);
                        }
                        catch (Exception ex)
                        {
                            downloadsModelItem.GID = "Error - the information is not in the database";
                            downloadsModelItem.StartDownloadAt = "Error - the information is not in the database";
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
                    foreach (var downloadFile in await _aria2Client.TellStopped())
                    {
                        var downloadsModelItem = new DownloadProgressModel();
                        downloadsModelItem.Name = downloadFile.Files[0].Path;

                        try
                        {
                            var currentDownloadLink = _movieContextReadOnly.Movies
                            .Where(Q => Q.DownloadId == downloadFile.GID)
                            .Include(In => In.TranscoddedFiles)
                            .First();

                            downloadsModelItem.GID = currentDownloadLink.ArTitle;
                            downloadsModelItem.logo = currentDownloadLink.ImgThumbObjUrl;
                            downloadsModelItem.StartDownloadAt = HelperFunctions.GetElapsedTime(currentDownloadLink.StartDownloadAt);
                        }
                        catch (Exception ex)
                        {
                            downloadsModelItem.GID = "Error - the information is not in the database";
                            downloadsModelItem.StartDownloadAt = "Error - the information is not in the database";
                        }



                        downloadsModelItem.DownloadSpeed = 0;
                        downloadsModelItem.DownloadSpeedHuman = "";
                        downloadsModelItem.CompletedLength = 0;

                        downloadsModelItem.Status = downloadFile.Status;
                        downloadsModelItem.Dir = downloadFile.Dir;
                        downloadsModelItem.downloadSizeHuman = "";
                        downloadsModelItem.TotalLength = 0;
                        downloadsModelItem.downloadStatus = downloadFile.Status;


                        if (downloadFile.Status == "complete")
                        {
                            downloadsModelItem.Percent = 100;

                        }

                        if (downloadFile.Status == "error")
                        {
                            downloadsModelItem.Percent = 0;

                        }

                        downloadsModel.Add(downloadsModelItem);
                    }



                    if (downloadsModel.Count == 0)
                    {

                    }
                    else {
                        await _downloadProgressHub.Clients.All.SendAsync("items", downloadsModel);

                    }


                } catch (Exception e) {
                
                }

                

            };
        }


        private void LoopThrowAria2Files(List<DownloadProgressModel> downloadsModel, DownloadStatusModel downloadFile)
        {
            
        }
    }
}
