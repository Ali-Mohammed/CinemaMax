using CinemaMaxFeeder.Database.Model;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaMaxFeeder.ModelJson
{
    [Table("Translation")]
    public partial class TranslationJson
    {

        [Key]
        public long Id { get; set; }

        [JsonProperty("id")]
        public long NB { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("extention")]
        public string Extention { get; set; }

        [JsonProperty("file")]
        public Uri File { get; set; }

        [JsonIgnore]
        public MovieDownloadStatus DownloadStatus { get; set; }

        [JsonIgnore]
        public DateTime DownloadStarted { get; set; }

        [JsonIgnore]
        public DateTime DownloadCompleted { get; set; }

        [JsonIgnore]
        public int DownloadRetry { get; set; }

        [JsonIgnore]
        public string DownloadId { get; set; }
    }
}