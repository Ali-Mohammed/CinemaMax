using Newtonsoft.Json;

namespace CinemaMaxFeeder.ModelJson
{

    public partial class SkippingDurationsJson
    {
        public long Id { get; set; }

        [JsonProperty("start")]
        public string[] Start { get; set; }

        [JsonProperty("end")]
        public string[] End { get; set; }
    }
}