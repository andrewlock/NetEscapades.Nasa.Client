using System;
using System.Collections.Generic;
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
        /// Fetch the metadata for a given asset NasaId
        /// </summary>
        /// <param name="nasaId">The NASA asset id to fetch the metadata for</param>
        /// <returns>The metadata as a json object</returns>
        public async Task<IResult<JObject>> GetAssetMetadata(string nasaId)
        {
            var locationResponse = await _apiClient.GetAsync("metadata/" + nasaId).ConfigureAwait(false);
            if (!locationResponse.IsSuccessStatusCode)
            {
                return Result<JObject>.Failure(
                    "Error retrieving metadata location from NASA API: " +
                    $"{(int)locationResponse.StatusCode} {locationResponse.ReasonPhrase}");
            }
            var locationContent = await locationResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            var metadataLocation = (string)JObject.Parse(locationContent)["location"];

            var metdataResponse = await _apiClient.GetAsync(metadataLocation).ConfigureAwait(false);
            if (!metdataResponse.IsSuccessStatusCode)
            {
                return Result<JObject>.Failure(
                    "Error retrieving metadata from NASA API: " +
                    $"{(int)metdataResponse.StatusCode} {metdataResponse.ReasonPhrase}");
            }

            var metadataContent = await metdataResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            var metadata = JObject.Parse(metadataContent);
            return Result<JObject>.Success(metadata);
        }
    }
}
