using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaMaxFeeder.ModelJson
{
    public partial class RsInfoJson
    {
        public long Id { get; set; }

        [JsonProperty("nb")]
        public long Nb { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("role")]
        public long Role { get; set; }

        [JsonProperty("staff_img")]
        public string StaffImg { get; set; }

        [JsonProperty("staff_img_thumb")]
        public string StaffImgThumb { get; set; }

        [JsonProperty("staff_img_medium_thumb")]
        public string StaffImgMediumThumb { get; set; }
    }
}