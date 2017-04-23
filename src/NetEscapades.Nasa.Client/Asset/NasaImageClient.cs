using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NetEscapades.Nasa
{
    public partial class NasaImageClient : INasaImageClient
    {
        /// <summary>
        /// Fetch the asset list manifest for a given asset NasaId
        /// This contains the list of assets available for the given asset Id. 
        /// Includes images of various sizes, the metadata files, and captions available
        /// </summary>
        /// <param name="nasaId">The NASA asset id to fetch the manifest for</param>
        /// <returns>The metadata as a json object</returns>
        public async Task<IResult<ICollection<string>>> GetAssetManifest(string nasaId)
        {
            var locationResponse = await _apiClient.GetAsync("asset/" + nasaId).ConfigureAwait(false);
            if (!locationResponse.IsSuccessStatusCode)
            {
                return Result<ICollection<string>>.Failure(
                    "Error retrieving manifest from NASA API: " +
                    $"{locationResponse.StatusCode} {locationResponse.ReasonPhrase}");
            }
            var content = await locationResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            var items = JObject.Parse(content)["collection"]["items"]
                .Select(x => (string)x["href"])
                .ToList();

            return Result<ICollection<string>>.Success(items);
        }

        /// <summary>
        /// Fetch the caption file for a given asset, such as a VTT or SRT 
        /// </summary>
        /// <param name="nasaId">The NASA asset id to fetch the captions for</param>
        /// <returns>The metadata as a json object</returns>
        public async Task<IResult<CaptionFile>> GetAssetCaptions(string nasaId)
        {
            var locationResponse = await _apiClient.GetAsync("captions/" + nasaId).ConfigureAwait(false);
            if (!locationResponse.IsSuccessStatusCode)
            {
                return Result<CaptionFile>.Failure(
                    "Error retrieving caption location from NASA API: " +
                    $"{(int)locationResponse.StatusCode} {locationResponse.ReasonPhrase}");
            }
            var locationContent = await locationResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            var metadataLocation = (string)JObject.Parse(locationContent)["location"];

            var captionResponse = await _apiClient.GetAsync(metadataLocation).ConfigureAwait(false);
            if (!captionResponse.IsSuccessStatusCode)
            {
                return Result<CaptionFile>.Failure(
                    "Error retrieving caption from NASA API: " +
                    $"{(int)captionResponse.StatusCode} {captionResponse.ReasonPhrase}");
            }

            var captionContent = await captionResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            var caption = new CaptionFile
            {
                NasaId = nasaId,
                Captions = captionContent,
                Location = metadataLocation,
                MimeType = captionResponse.Content.Headers
                .FirstOrDefault(x => string.Equals(x.Key, "Content-Type"))
                .Value.FirstOrDefault(),
            };
            return Result<CaptionFile>.Success(caption);
        }
    }
}
