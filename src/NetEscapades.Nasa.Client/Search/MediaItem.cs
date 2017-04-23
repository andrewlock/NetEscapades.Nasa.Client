using System.Collections.Generic;

namespace NetEscapades.Nasa
{
    public class Asset
    {
        public ICollection<Links> Links { get; set; }
        public ICollection<AssetDetails> Data { get; set; }
        public string Href { get; set; }
    }
}
