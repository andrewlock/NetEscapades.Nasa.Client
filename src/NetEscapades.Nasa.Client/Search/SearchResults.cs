using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetEscapades.Nasa
{
    public class SearchResults
    {
        public SearchResults(RawSearchResults rawData)
        {
            Raw = rawData;
        }

        /// <summary>
        /// The raw data returned by the NASA API endpoint as a strongly typed item
        /// </summary>
        public RawSearchResults Raw { get; }

        /// <summary>
        /// The total number of items found by the search
        /// </summary>
        public int TotalCount => Raw.Metadata.TotalHits;

        /// <summary>
        /// The collection of items returned by the search
        /// </summary>
        public IEnumerable<AssetDetails> Items => Raw.Items.SelectMany(x => x.Data);
    }
}
