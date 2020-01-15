using CinemaMaxFeeder.ModelJson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CinemaMaxFeeder.Database.Model
{

    public enum MovieDownloadStatus
    {
        Waiting,
        Started,
        Complete,
        Error,
        Pused
    }

    public partial class StorageServer
    {
        public long Id { get; set; }
        public string IP { get; set; }
        public string Path { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public string ServerLoad { get; set; }
        public string Comments { get; set; }
        public List<Movie> Movies { get; set; }
    }

    public partial class Movie
    {
        public long Id { get; set; }
        public long Nb { get; set; }
        public long Priority { get; set; }
        public bool IsSlideShow { get; set; }
        public string EnTitle { get; set; }
        public string ArTitle { get; set; }
        public string OtherTitle { get; set; }
        public Decimal Stars { get; set; }
        public string EnTranslationFile { get; set; }
        public string ArTranslationFile { get; set; }
        public string FileFile { get; set; }
        public string ArContent { get; set; }
        public string EnContent { get; set; }
        public DateTime MDate { get; set; }
        public long Year { get; set; }
        public long Kind { get; set; }
        public long Season { get; set; }
        public string Img { get; set; }
        public string ImgThumb { get; set; }
        public string ImgMediumThumb { get; set; }
        public Uri ImgObjUrl { get; set; }
        public Uri ImgMediumThumbObjUrl { get; set; }
        public long FilmRating { get; set; }
        public long SeriesRating { get; set; }
        public long EpisodeNummer { get; set; }
        public long Rate { get; set; }
        public long IsSpecial { get; set; }
        public DateTimeOffset ItemDate { get; set; }
        public string Duration { get; set; }
        public Uri ImdbUrlRef { get; set; }
        public Uri ImgBanner { get; set; }
        public long RootSeries { get; set; }
        public long UseParentImg { get; set; }
        public string SpTranslationFile { get; set; }
        public string ShowComments { get; set; }
        public string EpisodeFlag { get; set; }
        public Uri Trailer { get; set; }
        public long AudioStreamNumber { get; set; }
        public long ParentSkipping { get; set; }
        public long CollectionId { get; set; }
        public long IsDeleted { get; set; }
        public string CacheShort { get; set; }
        public Uri ImgThumbObjUrl { get; set; }
        public Uri ArTranslationFilePath { get; set; }
        public Uri EnTranslationFilePath { get; set; }
        public bool HasIntroSkipping { get; set; }
        public long VideoLikesNumber { get; set; }
        public long VideoDisLikesNumber { get; set; }
        public long VideoCommentsNumber { get; set; }
        public long VideoViewsNumber { get; set; }
        public bool Castable { get; set; }
        public DateTime StartDownloadAt { get; set; }
        public DateTime FinishDownloadAt { get; set; }
        public long DownloadRetry { get; set; }
        public string DownloadId { get; set; }

        public VideoLanguagesJson VideoLanguages { get; set; }
        public MovieDownloadStatus DownloadStatus { get; set; }



        public ICollection<SkippingDurations> SkippingDurationsStart { get; set; }
        public ICollection<TranslationJson> Translations { get; set; }
        public ICollection<IntroSkippingJson> IntroSkipping { get; set; }
        public ICollection<VideoLanguagesJson> Categories { get; set; }
        public ICollection<DirectorsInfo> DirectorsInfo { get; set; }
        public ICollection<ActorsInfo> ActorsInfo { get; set; }
        public ICollection<WritersInfo> WritersInfo { get; set; }
        public ICollection<CommentsJson> Comments { get; set; }
        public ICollection<TranscoddedFilesJson> TranscoddedFiles { get; set; }

        public List<HomePageSliderMovie> HomePageSliderMovies { get; set; }
        public StorageServer StorageServer { get; set; }



        [NotMapped]
        public string BannerFullUrl
        {
            get { return "http://192.168.1.150/S1/" + this.Id + "/" + Base64Encode(this.EnTitle) + "/img-banner.jpg"; }
        }

        [NotMapped]
        public string ImgObjUrlFull
        {
            get { return "http://192.168.1.150/S1/" + this.Id + "/" + Base64Encode(this.EnTitle) + "/" + this.Img; }
        }
        [NotMapped]
        public string ImgThumbObjUrlFull
        {
            get { return "http://192.168.1.150/S1/" + this.Id + "/" + Base64Encode(this.EnTitle) + "/" + this.ImgThumb; }
        }
        [NotMapped]
        public string ImgMediumThumbObjUrlFull
        {
            get { return "http://192.168.1.150/S1/" + this.Id + "/" + Base64Encode(this.EnTitle) + "/" + this.ImgMediumThumb; }
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

    }


    public partial class SkippingDurations
    {
        public long Id { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public Movie Movie { get; set; }
    }


    public partial class DirectorsInfo
    {
        public long Id { get; set; }
        public long Nb { get; set; }
        public string Name { get; set; }
        public long Role { get; set; }
        public string StaffImg { get; set; }
        public string StaffImgThumb { get; set; }
        public string StaffImgMediumThumb { get; set; }
        public Movie Movie { get; set; }
    }
    public partial class ActorsInfo
    {
        public long Id { get; set; }
        public long Nb { get; set; }
        public string Name { get; set; }
        public long Role { get; set; }
        public string StaffImg { get; set; }
        public string StaffImgThumb { get; set; }
        public string StaffImgMediumThumb { get; set; }
        public Movie Movie { get; set; }
    }
    public partial class WritersInfo
    {
        public long Id { get; set; }
        public long Nb { get; set; }
        public string Name { get; set; }
        public long Role { get; set; }
        public string StaffImg { get; set; }
        public string StaffImgThumb { get; set; }
        public string StaffImgMediumThumb { get; set; }
        public Movie Movie { get; set; }
    }



}
