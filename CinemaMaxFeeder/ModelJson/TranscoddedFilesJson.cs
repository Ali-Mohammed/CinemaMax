using CinemaMaxFeeder.Database.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CinemaMaxFeeder.ModelJson
{
    [Table("TranscoddedFiles")]
    public class TranscoddedFilesJson
    {
        [Key]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("resolution")]
        public string Resolution { get; set; }

        [JsonProperty("container")]
        public string Container { get; set; }

        [JsonProperty("transcoddedFileName")]
        public string TranscoddedFileName { get; set; }

        [JsonProperty("videoUrl")]
        public Uri VideoUrl { get; set; }

        [JsonIgnore]
        public MovieDownloadStatus DownloadStatus { get; set; }

        [JsonIgnore]
        public string DownloadId { get; set; }

        [JsonIgnore]
        public DateTime StartDownloadAt { get; set; }

        [JsonIgnore]
        public DateTime FinishDownloadAt { get; set; }

        [JsonIgnore]
        public long DownloadRetry { get; set; }

        [JsonIgnore]
        public string DownloadLocalPath { get; set; }

        [JsonIgnore]
        public long ServerId { get; set; }

        [JsonIgnore]
        public long FileSize { get; set; }
    }
}
