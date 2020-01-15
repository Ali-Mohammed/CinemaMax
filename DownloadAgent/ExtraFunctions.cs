using CinemaMaxFeeder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DownloadAgent
{
    class ExtraFunctions
    {
        public static void LoadAllMediaAgain()
        {

            Console.WriteLine("Function Starting.....");
            var movieContext = new MovieContext();

            var getMovies = movieContext
                .Movies
                .Where(Q => Q.DownloadStatus  == CinemaMaxFeeder.Database.Model.MovieDownloadStatus.Complete)
                .Include(c => c.TranscoddedFiles)
                .Include(c => c.Translations)
                .ToList();


            getMovies.ForEach(async movie => {
                Console.WriteLine(movie.EnTitle);
                await AddMoviesToTheQueue.DownloadMediaFilesAsync(movie);
            });
        }

        public static void LoadMediaById(int Id)
        {

            Console.WriteLine("Function Starting.....");
            var movieContext = new MovieContext();

            var getMovies = movieContext
                .Movies
                .Where(Q => Q.DownloadStatus == CinemaMaxFeeder.Database.Model.MovieDownloadStatus.Complete)
                .Where(Q => Q.Id == Id)
                .Include(c => c.TranscoddedFiles)
                .Include(c => c.Translations)
                .ToList();


            getMovies.ForEach(async movie => {
                Console.WriteLine(movie.EnTitle);
                await AddMoviesToTheQueue.DownloadMediaFilesAsync(movie);
            });
        }


    }
}
