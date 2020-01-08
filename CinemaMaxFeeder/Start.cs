using CinemaMaxFeeder.Database.Model;
using CinemaMaxFeeder.ModelJson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace CinemaMaxFeeder
{
    class Start
    {
        private TargetURLs targetURLs;
        private MovieContext _context;

        public Start()
        {
            this.targetURLs = new TargetURLs();
            this._context = new MovieContext();
        }

        public int getPageNumber()
        {

            var movieCount = _context.Movies.Count();

            if (movieCount == 0)
            {
                return 0;
            }



            int pageNumber = movieCount / Config.ItemPerPage;
            Console.WriteLine("Page Number: " + pageNumber);

            return pageNumber;
        }

        public void Pulling()
        {
            

            var reqUrl = targetURLs.GetMovieListURL(Config.ItemPerPage, getPageNumber());
            var movies = RequestHelper.Call<List<MovieJson>>(reqUrl);

            foreach (MovieJson movie in movies)
            {
                //Check if movie exsist
                if (_context.Movies.Where(q => q.Nb == movie.Nb).Count() == 0)
                {
                    GetFullMovieInformation(movie, false);
                }
                else {
                    Console.WriteLine("Movie is already exist in the Database: " + movie.EnTitle);
                }
                
            }

            Console.WriteLine("++++++++ LOOP DONE ++++++++++");
        }

        public void PullingSlideShow()
        {


            var reqUrl = targetURLs.GetMovieSlideShowURL();
            var movies = RequestHelper.Call<List<MovieJson>>(reqUrl);

            foreach (MovieJson movie in movies)
            {
                //Check if movie exsist
                if (_context.Movies.Where(q => q.Nb == movie.Nb).Count() == 0)
                {
                    GetFullMovieInformation(movie, true);
                }
                else
                {
                    Console.WriteLine("Movie is already exist in the Database: " + movie.EnTitle);
                }

            }

            Console.WriteLine("++++++++ LOOP DONE ++++++++++");
        }

        private void GetFullMovieInformation(MovieJson movie, bool isSlideShow)
        {
            Console.WriteLine(movie.EnTitle);

            var reqUrl = targetURLs.GetMovieInfoURL(movie.Nb);
            var movieFullInfo = RequestHelper.Call<MovieJson>(reqUrl);

            SaveInformationIntoTheDatabase(movieFullInfo, isSlideShow, movie);

        }

        private void SaveInformationIntoTheDatabase(MovieJson movieFullInfo, bool isSlideShow, MovieJson movieOrginalJson)
        {
            //Get the information for the movie
            List<SkippingDurations> skippingDurations = GetSkippingDurations(movieFullInfo);
            List<DirectorsInfo> directorsInfos = GetDirectorsInfo(movieFullInfo);
            List<ActorsInfo> actorsInfos = GetActorsInfo(movieFullInfo);
            List<WritersInfo> writersInfos = GetWritersInfo(movieFullInfo);

            var reqUrlComments = targetURLs.GetMovieCommentsURL(movieFullInfo.Nb);
            var movieComments = RequestHelper.Call<List<CommentsJson>>(reqUrlComments);

            var reqUrlTranCodes = targetURLs.GetMovieTransCodeURL(movieFullInfo.Nb);
            var movieTransCodes = RequestHelper.Call<List<TranscoddedFilesJson>>(reqUrlTranCodes);

            Movie movieModel = GetMovieModelSaveInformation(movieFullInfo, skippingDurations, directorsInfos, actorsInfos, writersInfos, movieComments, movieTransCodes);


            movieModel.IsSlideShow = isSlideShow;


            if (isSlideShow)
            {
                movieModel.Priority = 1;
                movieModel.ImgBanner = movieOrginalJson.ImgObjUrl;
            }

            _context.Movies.Add(movieModel);
            _context.SaveChanges();

            DownloadFileFromURL(movieFullInfo.ImgObjUrl.ToString(), movieModel.Id.ToString() + "/" + Base64Encode(movieModel.EnTitle),  movieFullInfo.Img);
            DownloadFileFromURL(movieFullInfo.ImgThumbObjUrl.ToString(), movieModel.Id.ToString() + "/" + Base64Encode(movieModel.EnTitle),  movieFullInfo.ImgThumb);
            DownloadFileFromURL(movieFullInfo.ImgMediumThumbObjUrl.ToString(), movieModel.Id.ToString() + "/" + Base64Encode(movieModel.EnTitle), movieFullInfo.ImgMediumThumb);


            if (isSlideShow)
            {
                DownloadFileFromURL(movieOrginalJson.ImgObjUrl.ToString(), movieModel.Id.ToString() + "/" + Base64Encode(movieModel.EnTitle), "img-banner.jpg");

            }

            if (movieFullInfo.Translations != null)
            {
                foreach (var trans in movieFullInfo.Translations)
                {
                    var fileName = trans.File.ToString().Split("?")[0].Split("/translation-files/")[1];

                    DownloadFileFromURL(trans.File.ToString(), movieModel.Id.ToString() + "/" + Base64Encode(movieModel.EnTitle), fileName);
                }
            }




        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        private static void DownloadFileFromURL(string url, string path, string fileName)
        {
            Console.WriteLine("DOWNLOAD FILE TO: " + Config.BaseFilePath + path + "/" + fileName);
            Directory.CreateDirectory(Config.BaseFilePath + path);
            WebClient client = new WebClient();
            client.DownloadFileAsync(new Uri(url), Config.BaseFilePath + path + "/" + fileName);
        }

        private static Movie GetMovieModelSaveInformation(MovieJson movieFullInfo, List<SkippingDurations> skippingDurations, List<DirectorsInfo> directorsInfos, List<ActorsInfo> actorsInfos, List<WritersInfo> writersInfos, List<CommentsJson> movieComments, List<TranscoddedFilesJson> movieTransCodes)
        {

            var movieModel = new Movie();
            movieModel.Nb = movieFullInfo.Nb;
            movieModel.EnTitle = movieFullInfo.EnTitle;
            movieModel.Priority = 0;
            movieModel.ArTitle = movieFullInfo.ArTitle;
            movieModel.OtherTitle = movieFullInfo.OtherTitle;
            movieModel.Stars = movieFullInfo.Stars;
            movieModel.EnTranslationFile = movieFullInfo.EnTranslationFile;
            movieModel.ArTranslationFile = movieFullInfo.ArTranslationFile;
            movieModel.FileFile = movieFullInfo.FileFile;
            movieModel.ArContent = movieFullInfo.ArContent;
            movieModel.EnContent = movieFullInfo.EnContent;
            movieModel.MDate = movieFullInfo.MDate;
            movieModel.Year = movieFullInfo.Year;
            movieModel.Kind = movieFullInfo.Kind;
            movieModel.Season = movieFullInfo.Season;
            movieModel.Img = movieFullInfo.Img;
            movieModel.ImgThumb = movieFullInfo.ImgThumb;
            movieModel.ImgMediumThumb = movieFullInfo.ImgMediumThumb;
            movieModel.ImgObjUrl = movieFullInfo.ImgObjUrl;
            movieModel.ImgMediumThumbObjUrl = movieFullInfo.ImgMediumThumbObjUrl;
            movieModel.FilmRating = movieFullInfo.FilmRating;
            movieModel.SeriesRating = movieFullInfo.SeriesRating;
            movieModel.EpisodeNummer = movieFullInfo.EpisodeNummer;
            movieModel.Rate = movieFullInfo.Rate;
            movieModel.IsSpecial = movieFullInfo.IsSpecial;
            movieModel.ItemDate = movieFullInfo.ItemDate;
            movieModel.Duration = movieFullInfo.Duration;
            movieModel.ImdbUrlRef = movieFullInfo.ImdbUrlRef;
            movieModel.RootSeries = movieFullInfo.RootSeries;
            movieModel.UseParentImg = movieFullInfo.UseParentImg;
            movieModel.SpTranslationFile = movieFullInfo.SpTranslationFile;
            movieModel.ShowComments = movieFullInfo.ShowComments;
            movieModel.EpisodeFlag = movieFullInfo.EpisodeFlag;
            movieModel.Trailer = movieFullInfo.Trailer;
            movieModel.AudioStreamNumber = movieFullInfo.AudioStreamNumber;
            movieModel.ParentSkipping = movieFullInfo.ParentSkipping;
            movieModel.CollectionId = movieFullInfo.CollectionId;
            movieModel.IsDeleted = movieFullInfo.IsDeleted;
            movieModel.CacheShort = movieFullInfo.CacheShort;
            movieModel.ImgThumbObjUrl = movieFullInfo.ImgThumbObjUrl;
            movieModel.SkippingDurationsStart = skippingDurations;
            movieModel.ArTranslationFilePath = movieFullInfo.ArTranslationFilePath;
            movieModel.EnTranslationFilePath = movieFullInfo.EnTranslationFilePath;
            movieModel.Translations = movieFullInfo.Translations;
            movieModel.HasIntroSkipping = movieFullInfo.HasIntroSkipping;
            movieModel.IntroSkipping = movieFullInfo.IntroSkipping;
            movieModel.Categories = movieFullInfo.Categories;
            movieModel.VideoLikesNumber = movieFullInfo.VideoLikesNumber;
            movieModel.VideoDisLikesNumber = movieFullInfo.VideoDisLikesNumber;
            movieModel.VideoLanguages = movieFullInfo.VideoLanguages;
            movieModel.VideoCommentsNumber = movieFullInfo.VideoCommentsNumber;
            movieModel.VideoViewsNumber = movieFullInfo.VideoViewsNumber;
            movieModel.DirectorsInfo = directorsInfos;
            movieModel.ActorsInfo = actorsInfos;
            movieModel.WritersInfo = writersInfos;
            movieModel.Castable = movieFullInfo.Castable;
            movieModel.DownloadStatus = MovieDownloadStatus.Waiting;
            movieModel.Comments = movieComments;
            movieModel.TranscoddedFiles = movieTransCodes;
            return movieModel;
        }

        private static List<WritersInfo> GetWritersInfo(MovieJson movieFullInfo)
        {
            List<WritersInfo> writersInfos = new List<WritersInfo>();
            if (movieFullInfo.WritersInfo != null)
            {
                for (var index = 0; index < movieFullInfo.WritersInfo.Length; index++)
                {
                    var addItem = new WritersInfo();
                    addItem.Nb = movieFullInfo.WritersInfo[index].Nb;
                    addItem.Name = movieFullInfo.WritersInfo[index].Name;
                    addItem.Role = movieFullInfo.WritersInfo[index].Role;
                    addItem.StaffImg = movieFullInfo.WritersInfo[index].StaffImg;
                    addItem.StaffImgThumb = movieFullInfo.WritersInfo[index].StaffImgThumb;
                    writersInfos.Add(addItem);
                }
            }

            return writersInfos;
        }

        private static List<ActorsInfo> GetActorsInfo(MovieJson movieFullInfo)
        {
            List<ActorsInfo> actorsInfos = new List<ActorsInfo>();
            if (movieFullInfo.ActorsInfo != null)
            {
                for (var index = 0; index < movieFullInfo.ActorsInfo.Length; index++)
                {
                    var addItem = new ActorsInfo();
                    addItem.Nb = movieFullInfo.ActorsInfo[index].Nb;
                    addItem.Name = movieFullInfo.ActorsInfo[index].Name;
                    addItem.Role = movieFullInfo.ActorsInfo[index].Role;
                    addItem.StaffImg = movieFullInfo.ActorsInfo[index].StaffImg;
                    addItem.StaffImgThumb = movieFullInfo.ActorsInfo[index].StaffImgThumb;
                    actorsInfos.Add(addItem);
                }
            }

            return actorsInfos;
        }

        private static List<DirectorsInfo> GetDirectorsInfo(MovieJson movieFullInfo)
        {
            List<DirectorsInfo> directorsInfos = new List<DirectorsInfo>();
            if (movieFullInfo.DirectorsInfo != null)
            {
                for (var index = 0; index < movieFullInfo.DirectorsInfo.Length; index++)
                {
                    var addItem = new DirectorsInfo();
                    addItem.Nb = movieFullInfo.DirectorsInfo[index].Nb;
                    addItem.Name = movieFullInfo.DirectorsInfo[index].Name;
                    addItem.Role = movieFullInfo.DirectorsInfo[index].Role;
                    addItem.StaffImg = movieFullInfo.DirectorsInfo[index].StaffImg;
                    addItem.StaffImgThumb = movieFullInfo.DirectorsInfo[index].StaffImgThumb;
                    directorsInfos.Add(addItem);
                }
            }

            return directorsInfos;
        }

        private static List<SkippingDurations> GetSkippingDurations(MovieJson movieFullInfo)
        {
            List<SkippingDurations> skippingDurationsStart = new List<SkippingDurations>();
            if (movieFullInfo.SkippingDurations != null)
            {

                for (var index = 0; index < movieFullInfo.SkippingDurations.Start.Length; index++)
                {
                    var addItem = new SkippingDurations();
                    addItem.Start = movieFullInfo.SkippingDurations.Start[index];
                    addItem.End = movieFullInfo.SkippingDurations.End[index];
                    skippingDurationsStart.Add(addItem);
                }
            }

            return skippingDurationsStart;
        }
    }
}
