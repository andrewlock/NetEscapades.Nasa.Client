using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace NetEscapades.Nasa
{
    public partial interface INasaImageClient
    {
        /// <summary>
        /// Fetch the metadata for a given asset NasaId
        /// </summary>
        /// <param name="nasaId">The NASA asset id to fetch the metadata for</param>
        /// <returns>The metadata as a json object</returns>
        Task<IResult<JObject>> GetAssetMetadata(string nasaId);
    }
}
