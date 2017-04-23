using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using NetEscapades.Nasa.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NetEscapades.Nasa
{
    public partial class NasaImageClient : INasaImageClient
    {
        /// <summary>
        /// Search for images on NASA
        /// </summary>
        /// <param name="query">Free text search terms to compare to all indexed metadata</param>
        /// <returns></returns>
        public Task<IResult<SearchResults>> Search(string query)
        {
            return Search(new SearchQuery { Query = query });
        }

        /// <summary>
        /// Search for images on NASA
        /// </summary>
        /// <param name="query">Parameters defining the search</param>
        /// <returns></returns>
        public async Task<IResult<SearchResults>> Search(SearchQuery query)
        {
            var uri = QueryHelpers.AddQueryString("search", query.ToDictionary());
            var response = await _apiClient.GetAsync(uri).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                return Result<SearchResults>.Failure($"Error from NASA API: {(int)response.StatusCode} {response.ReasonPhrase}");
            }

            // read JSON directly from stream
            var serializer = new JsonSerializer();

            RawSearchResultsRoot root;
            using (var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
            using (var sr = new StreamReader(stream))
            using (var reader = new JsonTextReader(sr))
            {
                root = serializer.Deserialize<RawSearchResultsRoot>(reader);
            }

            return Result<SearchResults>.Success(new SearchResults(root.Collection));
        }

        /// <summary>
        /// Get a list of NASA asset ids for recent assets added to NASA image database
        /// </summary>
        public async Task<IResult<ICollection<string>>> GetRecentAssetIds()
        {
            var response = await _apiClient.GetAsync("asset/?orderby=recent").ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                return Result<ICollection<string>>.Failure(
                    "Error retrieving recent assets from NASA API: " +
                    $"{response.StatusCode} {response.ReasonPhrase}");
            }
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var items = JObject.Parse(content)["collection"]["items"]
                .Select(x => {
                    var href = (string)x;
                    var end = href.Substring(href.IndexOf("/image/") + 7);
                    return end.Substring(0, end.IndexOf("/collection.json"));
                })
                .ToList();

            return Result<ICollection<string>>.Success(items);
        }

        /// <summary>
        /// Get a list of NASA asset ids for popular assets added to NASA image database
        /// </summary>
        public async Task<IResult<ICollection<string>>> GetPopularAssetIds()
        {
            var response = await _apiClient.GetAsync("asset/?orderby=popular").ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                return Result<ICollection<string>>.Failure(
                    "Error retrieving recent assets from NASA API: " +
                    $"{response.StatusCode} {response.ReasonPhrase}");
            }
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var items = JObject.Parse(content)["collection"]["items"]
                .Select(x => {
                    var href = (string)x;
                    var end = href.Substring(href.IndexOf("/image/") + 7);
                    return end.Substring(0, end.IndexOf("/collection.json"));
                })
                .ToList();

            return Result<ICollection<string>>.Success(items);
        }
    }
}
