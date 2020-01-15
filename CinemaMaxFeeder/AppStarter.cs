using CinemaMaxFeeder.Database.Model;
using CinemaMaxFeeder.ModelJson.HomePageSlider;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CinemaMaxFeeder
{
    public class AppStarter
    {
        private  readonly MovieContext context;

        public AppStarter()
        {
             context = new MovieContext();
        }

    
        public async System.Threading.Tasks.Task RunAsync()
        {

            Console.WriteLine("CHECK STORAGE");
            if (await context.StorageServers.CountAsync() == 0)
            {
                var newStorage = new StorageServer
                {
                    ServerLoad = "50",
                    Name = "default",
                    Comments = "default",
                    IP = "default",
                    Path = "default",
                    Size = "default",
                    Url = "default"
                };

                await context.StorageServers.AddAsync(newStorage);
                await context.SaveChangesAsync();
            }
            Console.WriteLine("END CHECK STORAGE");


            // await PullHomePageSliderFormTheSourceAsync();



          //  RunProgramForSlideShow();
            //await RunProgramForSeriesAsync();
            //await RunProgramForMoviesAsync();

            await RunProgramForSeriesEpisodeAsync();
        }

  

        private static async System.Threading.Tasks.Task PullHomePageSliderFormTheSourceAsync()
        {
            var start = new PullHomePageSliderFormTheSource();
            await start.Start();
        }

        private static void RunProgramForSlideShow()
        {
            var start = new PullMoviesFormTheSource();
            start.PullingSlideShow();
        }

        public async System.Threading.Tasks.Task RunProgramForMoviesAsync()
        {
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromSeconds(1);
            var PageNumber = 1004;

            var start = new PullMoviesFormTheSource();

            while (true) 
            
            {
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine("+++++++++++++++++++ START LOOP MVOIES +++++++++++++++++++++++++++++");
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");

                var count = await context.Movies.Where(Q => Q.Kind == 1).CountAsync();
                Console.WriteLine(count);

                Console.WriteLine(PageNumber);

                start.Pulling(PageNumber);

                PageNumber++;

            }


         
        }
        public async System.Threading.Tasks.Task RunProgramForSeriesAsync()
        {
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromSeconds(1);
            var PageNumber = 0;

            var start = new PullMoviesFormTheSource();

            while (true)

            {
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine("++++++++++++++++++++ START LOOP SERIES +++++++++++++++++++++++++++++");
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");

                var count = await context.Movies.Where(Q => Q.Kind == 12).CountAsync();
                Console.WriteLine(count);

                Console.WriteLine(PageNumber);

                start.PullingSeries(PageNumber);

                PageNumber++;

            }



        }
        public async System.Threading.Tasks.Task RunProgramForSeriesEpisodeAsync()
        {
            var start = new PullMoviesFormTheSource();


                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine("++++++++++++++++++++ START LOOP SERIES EPISODE ++++++++++++++++++++");
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");

                var count = await context.Movies.Where(Q => Q.Kind == 12).CountAsync();
                Console.WriteLine(count);

               await start.PullingSeriesEpisodeAsync();

        }
    }
}
