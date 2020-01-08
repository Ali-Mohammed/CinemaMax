using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaMaxFeeder.ModelJson
{
    [Table("VideoLanguages")]
    public partial class VideoLanguagesJson
    {
        public long Id { get; set; }

        [JsonProperty("en_title")]
        public string EnTitle { get; set; }

        [JsonProperty("ar_title")]
        public string ArTitle { get; set; }
    }
}