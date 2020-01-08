using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CinemaMaxFeeder.ModelJson
{
    [Table("Comments")]
    public class CommentsJson
    {
        [Key]
        public long Id { get; set; }

        [JsonProperty("nb")]
        public long Nb { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("mitgliedNb")]
        public long MitgliedNb { get; set; }

        [JsonProperty("mitgliedName")]
        public string MitgliedName { get; set; }

        [JsonProperty("mitgliedImg")]
        public string MitgliedImg { get; set; }

        [JsonProperty("mitgliedThumbImg")]
        public string MitgliedThumbImg { get; set; }

        [JsonProperty("imgObjUrl")]
        public Uri ImgObjUrl { get; set; }

        [JsonProperty("imgThumbObjUrl")]
        public Uri ImgThumbObjUrl { get; set; }

        [JsonProperty("objectUrlExpiration")]
        public string ObjectUrlExpiration { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("commentDate")]
        public DateTime CommentDate { get; set; }

        [JsonProperty("updateDateTime")]
        public long UpdateDateTime { get; set; }

        [JsonProperty("commentNb")]
        public long CommentNb { get; set; }

        [JsonProperty("permission")]
        public long Permission { get; set; }
    }
}
