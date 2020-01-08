using System;
using System.Linq;

namespace CinemaMaxFeeder
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("++++++++++++++++++++++++++ START +++++++++++++++++++++++++++++++++++");
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");

            //First the start pulling the banner movies!
            var start = new Start();
            start.PullingSlideShow();

            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("++++++++++++++++++++++++++ DONE +++++++++++++++++++++++++++++++++++");
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");




            var context = new MovieContext();
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromMinutes(3);
            
            var timer = new System.Threading.Timer((e) =>
            {
                Console.WriteLine("Loop....");

                var count = context.Movies.Count();
                Console.WriteLine(count);

                RunProgram();

            }, null, startTimeSpan, periodTimeSpan);


            Console.ReadLine();
            
        }


        public static void RunProgram()
        {
            var start = new Start();
            start.Pulling();
            Console.ReadLine();
        }
    }
}
