using System;
using System.Diagnostics;

namespace DownloadAgent
{
    class Program
    {
        static void Main(string[] args)
        {


            //ExtraFunctions.LoadAllMediaAgain();
           // Console.ReadKey();
           // return;

            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromSeconds(60);

            var timer = new System.Threading.Timer(async (e) =>
            {
                Console.WriteLine("--------------------------LOOP START--------------------------");
                await addMoveAsync();
                Console.WriteLine("--------------------------LOOP END--------------------------");

            }, null, startTimeSpan, periodTimeSpan);

            Console.WriteLine("START");
            Console.ReadLine();

        }

        public static async System.Threading.Tasks.Task addMoveAsync()
        {
            Console.WriteLine("START LOOP...");

            AddMoviesToTheQueue addMoviesToTheQueue = new AddMoviesToTheQueue();
            await addMoviesToTheQueue.AddMovieToTheDownloadQueueAsync();
        }
    }
}
