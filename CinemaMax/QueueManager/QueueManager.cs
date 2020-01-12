using CinemaMax.DownloadManager;
using CinemaMaxFeeder;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaMax.QueueManager
{
    public class QueueManagerClient
    {
        public static void StartQueueManager()
        {
            if (Config.StartJobsInBackground)
            {

              //  RecurringJob.AddOrUpdate(() =>  PullMoviesFromSourceInnerFunction(), "*/5 * * * *");
              //  RecurringJob.AddOrUpdate(() => PullMovieBannerFromSourceInnerFunction(), Cron.Hourly);

            }

        }

        public static void PullMovieBannerFromSourceInnerFunction()
        {
            //CinemaMaxFeeder.PullMoviesFormTheSource start = new CinemaMaxFeeder.PullMoviesFormTheSource();
           // start.PullingSlideShow();
        }

        public static void PullMoviesFromSourceInnerFunction()
        {
          //  CinemaMaxFeeder.PullMoviesFormTheSource start = new CinemaMaxFeeder.PullMoviesFormTheSource();
          //  start.Pulling();
        }
    }
}
