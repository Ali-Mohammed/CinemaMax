using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaMaxFeeder
{
    public class TargetURLs
    {
        public string GetMovieListURL(int ItemPerPage, int PageNumber)
        {
            return Config.BaseURL + "video/videoKind/1/itemsPerPage/" + ItemPerPage + "/categoryNb/0/langNb/0/sortParam/desc/searchTextValue/-/pageNumber/" + PageNumber + "/level/0";
        }

        public string GetSeriesListURL(int ItemPerPage, int PageNumber)
        {
            return Config.BaseURL + "video/videoKind/2/itemsPerPage/" + ItemPerPage + "/categoryNb/0/langNb/0/sortParam/desc/searchTextValue/-/pageNumber/" + PageNumber + "/level/0";
        }


        public string GetMovieInfoURL(long id)
        {
            return Config.BaseURL + "allVideoInfo/id/" + id;
        }

        public string GetMovieCommentsURL(long id)
        {
            return Config.BaseURL + "videoComment/id/" + id;
        }

        public string GetSeriesEpisodesURL(long id)
        {
            return Config.BaseURL + "videoSeason/id/" + id;
        }

        public string GetMovieTransCodeURL(long id)
        {
            return Config.BaseURL + "transcoddedFiles/id/" + id;
        }

        public string GetMovieSlideShowURL()
        {
            return Config.BaseURL + "banner/level/0";
        }

        public string GetMovieCategoryURL()
        {
            return Config.BaseURL + "categoryLangFreqVideo";
        }

    }
}
