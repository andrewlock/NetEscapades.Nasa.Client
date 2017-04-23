using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NetEscapades.Nasa
{
    public class AssetDetails
    {
        public string Center { get; set; }
        [JsonProperty("date_created")]
        public DateTime DateCreated { get; set; }
        public string Description { get; set; }
        public ICollection<string> Keywords { get; set; }
        [JsonProperty("media_type")]
        public string MediaType { get; set; }
        [JsonProperty("nasa_id")]
        public string NasaId { get; set; }
        public string Title { get; set; }
    }
}
