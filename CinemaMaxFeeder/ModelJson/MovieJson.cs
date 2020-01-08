using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text;

namespace CinemaMaxFeeder.ModelJson
{
    [Table("Movies")]
    public partial class MovieJson
    {
        [JsonProperty("nb")]
        public long Nb { get; set; }

        [JsonProperty("en_title")]
        public string EnTitle { get; set; }

        [JsonProperty("ar_title")]
        public string ArTitle { get; set; }

        [JsonProperty("other_title")]
        public string OtherTitle { get; set; }

        [JsonProperty("stars")]
        public string Stars { get; set; }

        [JsonProperty("enTranslationFile")]
        public string EnTranslationFile { get; set; }

        [JsonProperty("arTranslationFile")]
        public string ArTranslationFile { get; set; }

        [JsonProperty("fileFile")]
        public string FileFile { get; set; }

        [JsonProperty("ar_content")]
        public string ArContent { get; set; }

        [JsonProperty("en_content")]
        public string EnContent { get; set; }

        [JsonProperty("mDate")]
        public DateTime MDate { get; set; }

        [JsonProperty("year")]
        public long Year { get; set; }

        [JsonProperty("kind")]
        public long Kind { get; set; }

        [JsonProperty("season")]
        public long Season { get; set; }

        [JsonProperty("img")]
        public string Img { get; set; }

        [JsonProperty("imgThumb")]
        public string ImgThumb { get; set; }

        [JsonProperty("imgMediumThumb")]
        public string ImgMediumThumb { get; set; }

        [JsonProperty("imgObjUrl")]
        public Uri ImgObjUrl { get; set; }

        [JsonProperty("imgMediumThumbObjUrl")]
        public Uri ImgMediumThumbObjUrl { get; set; }

        [JsonProperty("filmRating")]
        public long FilmRating { get; set; }

        [JsonProperty("seriesRating")]
        public long SeriesRating { get; set; }

        [JsonProperty("episodeNummer")]
        public long EpisodeNummer { get; set; }

        [JsonProperty("rate")]
        public long Rate { get; set; }

        [JsonProperty("isSpecial")]
        public long IsSpecial { get; set; }

        [JsonProperty("itemDate")]
        public DateTimeOffset ItemDate { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("imdbUrlRef")]
        public Uri ImdbUrlRef { get; set; }

        [JsonProperty("rootSeries")]
        public long RootSeries { get; set; }

        [JsonProperty("useParentImg")]
        public long UseParentImg { get; set; }

        [JsonProperty("spTranslationFile")]
        public string SpTranslationFile { get; set; }

        [JsonProperty("showComments")]
        public string ShowComments { get; set; }

        [JsonProperty("episode_flag")]
        public string EpisodeFlag { get; set; }

        [JsonProperty("trailer")]
        public Uri Trailer { get; set; }

        [JsonProperty("audioStreamNumber")]
        public long AudioStreamNumber { get; set; }

        [JsonProperty("parent_skipping")]
        public long ParentSkipping { get; set; }

        [JsonProperty("collectionID")]
        public long CollectionId { get; set; }

        [JsonProperty("isDeleted")]
        public long IsDeleted { get; set; }

        [JsonProperty("CACHE_SHORT")]
        public string CacheShort { get; set; }

        [JsonProperty("imgThumbObjUrl")]
        public Uri ImgThumbObjUrl { get; set; }

        [JsonProperty("skippingDurations")]
        public SkippingDurationsJson SkippingDurations { get; set; }

        [JsonProperty("arTranslationFilePath")]
        public Uri ArTranslationFilePath { get; set; }

        [JsonProperty("enTranslationFilePath")]
        public Uri EnTranslationFilePath { get; set; }

        [JsonProperty("translations")]
        public TranslationJson[] Translations { get; set; }

        [JsonProperty("hasIntroSkipping")]
        public bool HasIntroSkipping { get; set; }

        [JsonProperty("introSkipping")]
        public IntroSkippingJson[] IntroSkipping { get; set; }

        [JsonProperty("categories")]
        public VideoLanguagesJson[] Categories { get; set; }

        [JsonProperty("videoLikesNumber")]
        public long VideoLikesNumber { get; set; }

        [JsonProperty("videoDisLikesNumber")]
        public long VideoDisLikesNumber { get; set; }

        [JsonProperty("videoLanguages")]
        public VideoLanguagesJson VideoLanguages { get; set; }

        [JsonProperty("videoCommentsNumber")]
        public long VideoCommentsNumber { get; set; }

        [JsonProperty("videoViewsNumber")]
        public long VideoViewsNumber { get; set; }

        [JsonProperty("directorsInfo")]
        public RsInfoJson[] DirectorsInfo { get; set; }

        [JsonProperty("actorsInfo")]
        public RsInfoJson[] ActorsInfo { get; set; }

        [JsonProperty("writersInfo")]
        public RsInfoJson[] WritersInfo { get; set; }

        [JsonProperty("castable")]
        public bool Castable { get; set; }
    }


}