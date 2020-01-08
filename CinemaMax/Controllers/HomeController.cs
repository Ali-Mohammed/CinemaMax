using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CinemaMax.Models;
using CinemaMax.Hubs;
using Microsoft.AspNetCore.SignalR;
using GensouSakuya.Aria2.SDK;
using CinemaMaxFeeder;
using Microsoft.EntityFrameworkCore;
using CinemaMax.Helpers;
using CinemaMax.DownloadManager;

namespace CinemaMax.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHubContext<DownloadProgressHub> _downloadProgressHub;
        private readonly ILogger<HomeController> _logger;
        Aria2Client _aria2Client;

        private MovieContext _movieContext;
        private readonly MovieContext _movieContextReadOnly;
        public ILogger<DownloadProgress> LoggerDownloadProcessor;

        public HomeController(
            ILogger<HomeController> logger,
            IHubContext<DownloadProgressHub> downloadProgressHub,
            MovieContext _movieContext,
            ILogger<DownloadProgress> loggerDownloadProcessor
            )
        {
            _logger = logger;
            this._aria2Client = new Aria2Client("http://127.0.0.1", 6800);
            _logger.LogInformation("amohammed " + this._aria2Client);
            _downloadProgressHub = downloadProgressHub;
            this._movieContext = _movieContext;
            this.LoggerDownloadProcessor = loggerDownloadProcessor;
            this._movieContextReadOnly = new MovieContext();
        }


        public async Task<IActionResult> IndexAsync()
        {
            DownloadProgress downloadProcessor = new DownloadProgress(LoggerDownloadProcessor, _downloadProgressHub);
            downloadProcessor.StartDownload();
            return Ok(new { Message = "Request Completed" });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
