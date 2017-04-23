using System.Collections.Generic;

namespace NetEscapades.Nasa
{
    public class RawSearchResults
    {
        public ICollection<Links> Links { get; set; }
        public string Version { get; set; }
        public string Href { get; set; }
        public SearchMetadata Metadata { get; set; }
        public ICollection<Asset> Items { get; set; }
    }
}
