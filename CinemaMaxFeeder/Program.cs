using System;
using System.Linq;

namespace CinemaMaxFeeder
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {

            AppStarter app = new AppStarter();
            await app.RunAsync();

            Console.ReadKey();

        }
    }
}
