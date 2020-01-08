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
                RecurringJob.AddOrUpdate(
                    () =>
                    addMoveAsync(),
                Cron.Minutely);
            }
        }

        public static async Task addMoveAsync()
        {
            AddMoviesToTheQueue addMoviesToTheQueue = new AddMoviesToTheQueue();
            await addMoviesToTheQueue.AddMovieToTheDownloadQueueAsync();
            Console.WriteLine("START THE QUEUE FOR ADDING FILE");
        }
    }
}
