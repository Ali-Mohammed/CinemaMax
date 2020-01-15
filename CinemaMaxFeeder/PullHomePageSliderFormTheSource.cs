using CinemaMaxFeeder.Database.Model;
using CinemaMaxFeeder.ModelJson;
using CinemaMaxFeeder.ModelJson.HomePageSlider;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace CinemaMaxFeeder
{
    public class PullHomePageSliderFormTheSource
    {
        private TargetURLs targetURLs;
        private MovieContext _context;

        public PullHomePageSliderFormTheSource()
        {
            this.targetURLs = new TargetURLs();
            this._context = new MovieContext();
        }

        public async System.Threading.Tasks.Task Start()
        {
            await ResetTheHomePageSliderAsync();
            await PullDefault();
            await PullCustom();
            await PullPopular();
        }


        public async System.Threading.Tasks.Task ResetTheHomePageSliderAsync()
        {
            foreach (var item in await _context.HomePageSliderMovie.ToListAsync())
            {
                _context.HomePageSliderMovie.Remove(item);
                await _context.SaveChangesAsync();
            }

            foreach (var item in await _context.HomePageSliders.ToListAsync())
            {
                _context.HomePageSliders.Remove(item);
                await _context.SaveChangesAsync();
            }
        }


        public async System.Threading.Tasks.Task PullDefault()
        {

            var reqUrl = targetURLs.GetHomePageSliderURL();
            var movies = RequestHelper.Call<HomePageSliderJson>(reqUrl);


            foreach (var item in movies.Group)
            {

                var homePageModel = new HomePageSlider();
                Console.WriteLine(item.Key);

                List<HomePageSliderMovie> homePageSliderMovies = new List<HomePageSliderMovie>();

                foreach (var content in movies.Group[item.Key].Content)
                {
                    var contentModel = new HomePageSliderMovie();

                    Console.WriteLine(content.ArTitle);

                    contentModel.Movie = await _context.Movies.Where(Q => Q.Nb == content.Nb).FirstOrDefaultAsync();
                    contentModel.HomePageSlider = homePageModel;
                    homePageSliderMovies.Add(contentModel);
                }


                homePageModel.Comments = "";
                homePageModel.Options = "";
                homePageModel.Type = "1";
                homePageModel.Name = item.Key;
                homePageModel.Order = 1;
                homePageModel.HomePageSliderMovies = homePageSliderMovies;

                _context.HomePageSliders.Add(homePageModel);
                _context.SaveChanges();
            }


        }
        public async System.Threading.Tasks.Task PullCustom()
        {

            var reqUrl = targetURLs.GetHomePageSliderCustomURL();
            var movies = RequestHelper.Call<HomePageSliderJson>(reqUrl);


            foreach (var item in movies.Group)
            {

                var homePageModel = new HomePageSlider();
                Console.WriteLine(item.Key);

                List<HomePageSliderMovie> homePageSliderMovies = new List<HomePageSliderMovie>();

                foreach (var content in movies.Group[item.Key].Content)
                {
                    var contentModel = new HomePageSliderMovie();

                    Console.WriteLine(content.ArTitle);

                    contentModel.Movie = await _context.Movies.Where(Q => Q.Nb == content.Nb).FirstOrDefaultAsync();
                    contentModel.HomePageSlider = homePageModel;
                    homePageSliderMovies.Add(contentModel);
                }


                homePageModel.Comments = "";
                homePageModel.Options = "";
                homePageModel.Type = "2";
                homePageModel.Name = item.Key;
                homePageModel.Order = 1;
                homePageModel.HomePageSliderMovies = homePageSliderMovies;

                _context.HomePageSliders.Add(homePageModel);
                _context.SaveChanges();
            }


        }
        public async System.Threading.Tasks.Task PullPopular()
        {

            var reqUrl = targetURLs.GetHomePageSliderPopularURL();
            var movies = RequestHelper.Call<HomePageSliderJson>(reqUrl);


            foreach (var item in movies.Group)
            {

                var homePageModel = new HomePageSlider();
                Console.WriteLine(item.Key);

                List<HomePageSliderMovie> homePageSliderMovies = new List<HomePageSliderMovie>();

                foreach (var content in movies.Group[item.Key].Content)
                {
                    var contentModel = new HomePageSliderMovie();

                    Console.WriteLine(content.ArTitle);

                    contentModel.Movie = await _context.Movies.Where(Q => Q.Nb == content.Nb).FirstOrDefaultAsync();
                    contentModel.HomePageSlider = homePageModel;
                    homePageSliderMovies.Add(contentModel);
                }


                homePageModel.Comments = "";
                homePageModel.Options = "";
                homePageModel.Type = "3";
                homePageModel.Name = movies.Group[item.Key].Name;
                homePageModel.Order = 1;
                homePageModel.HomePageSliderMovies = homePageSliderMovies;

                _context.HomePageSliders.Add(homePageModel);
                _context.SaveChanges();
            }


        }


    }
}
