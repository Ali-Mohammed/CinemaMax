using CinemaMaxFeeder.Database.Model;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaMaxFeeder.ModelJson
{
    [Table("IntroSkipping")]
    public partial class IntroSkippingJson
    {
        public long Id { get; set; }

        [JsonProperty("start")]
        public string Start { get; set; }

        [JsonProperty("end")]
        public string End { get; set; }

        [JsonProperty("control_level")]
        public string ControlLevel { get; set; }

        [JsonIgnore]
        public Movie Movie { get; set; }
    }
}