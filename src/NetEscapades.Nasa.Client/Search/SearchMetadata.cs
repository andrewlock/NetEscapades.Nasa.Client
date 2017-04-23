using Newtonsoft.Json;

namespace NetEscapades.Nasa
{
    public class SearchMetadata
    {
        [JsonProperty("total_hits")]
        public int TotalHits { get; set; }
    }
}
