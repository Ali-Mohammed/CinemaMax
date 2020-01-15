using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CinemaMaxFeeder.ModelJson.HomePageSlider
{
    public class HomePageSliderJson
    {

        [JsonProperty("group")]
        public Dictionary<string, HomePageSliderGroupJson> Group { get; set; }

        [JsonProperty("isLogin")]
        public bool IsLogin { get; set; }

        [JsonProperty("notify")]
        public string Notify { get; set; }

        [JsonProperty("isSessionExists")]
        public bool IsSessionExists { get; set; }

        [JsonProperty("mitglied_name")]
        public string MitgliedName { get; set; }

        [JsonProperty("mitglied_lighttigerImg")]
        public string MitgliedLighttigerImg { get; set; }
    }

    public class HomePageSliderGroupJson
    {
        [JsonProperty("content")]
        public List<HomePageSliderContentJson> Content { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }


    public class HomePageSliderContentJson
    {
        [JsonProperty("nb")]
        public long Nb { get; set; }

        [JsonProperty("ar_title")]
        [JsonPropertyName("ar_title")]
        public string ArTitle { get; set; }

        [JsonProperty("en_title")]
        [JsonPropertyName("en_title")]
        public string EnTitle { get; set; }

        [JsonProperty("other_title")]
        [JsonPropertyName("other_title")]
        public string OtherTitle { get; set; }

        [JsonProperty("stars")]
        public string Stars { get; set; }

        [JsonProperty("mDate")]
        public DateTimeOffset MDate { get; set; }

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

        [JsonProperty("imgThumbObjUrl")]
        public Uri ImgThumbObjUrl { get; set; }

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
        public string ItemDate { get; set; }

        [JsonProperty("videoUploadDate")]
        public DateTimeOffset VideoUploadDate { get; set; }

        [JsonProperty("language_lighttigerNb")]
        [JsonPropertyName("language_lighttigerNb")]
        public long LanguageLighttigerNb { get; set; }

        [JsonProperty("trailterFile")]
        public string TrailterFile { get; set; }

        [JsonProperty("imdbUrlRef")]
        public string ImdbUrlRef { get; set; }

        [JsonProperty("rootSeries")]
        public long RootSeries { get; set; }

        [JsonProperty("arTranslationFile")]
        public string ArTranslationFile { get; set; }

        [JsonProperty("enTranslationFile")]
        public string EnTranslationFile { get; set; }

        [JsonProperty("useParentImg")]
        public long UseParentImg { get; set; }

        [JsonProperty("showComments")]
        public long ShowComments { get; set; }

        [JsonProperty("episode_flag")]
        [JsonPropertyName("episode_flag")]
        public string EpisodeFlag { get; set; }

        [JsonProperty("objectUrlExpiration")]
        public DateTimeOffset ObjectUrlExpiration { get; set; }

        [JsonProperty("trailer")]
        public string Trailer { get; set; }

        [JsonProperty("parent_skipping")]
        [JsonPropertyName("parent_skipping")]
        public long ParentSkipping { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("translationFlag", NullValueHandling = NullValueHandling.Ignore)]
        public long? TranslationFlag { get; set; }

        [JsonProperty("item_order_list", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("item_order_list")]
        public long? ItemOrderList { get; set; }

        [JsonProperty("list_id", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("list_id")]
        public long? ListId { get; set; }

        [JsonProperty("list_sort_order", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("list_sort_order")]
        public long? ListSortOrder { get; set; }

        [JsonProperty("custom_ar_title", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("custom_ar_title")]
        public string? CustomArTitle { get; set; }

        [JsonProperty("custom_en_title", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("custom_en_title")]
        public string? CustomEnTitle { get; set; }

        [JsonProperty("audioStreamNumber", NullValueHandling = NullValueHandling.Ignore)]
        public long? AudioStreamNumber { get; set; }

        [JsonProperty("replacement", NullValueHandling = NullValueHandling.Ignore)]
        public long? Replacement { get; set; }

        [JsonProperty("collectionID", NullValueHandling = NullValueHandling.Ignore)]
        public long? CollectionId { get; set; }
    }

}
