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
        /// Fetch the asset list manifest for a given asset NasaId
        /// This contains the list of assets available for the given asset Id. 
        /// Includes images of various sizes, the metadata files, and captions available
        /// </summary>
        /// <param name="nasaId">The NASA asset id to fetch the manifest for</param>
        /// <returns>The metadata as a json object</returns>
        Task<IResult<ICollection<string>>> GetAssetManifest(string nasaId);

        /// <summary>
        /// Fetch the caption file for a given asset, such as a VTT or SRT 
        /// </summary>
        /// <param name="nasaId">The NASA asset id to fetch the captions for</param>
        /// <returns>The metadata as a json object</returns>
        Task<IResult<CaptionFile>> GetAssetCaptions(string nasaId);
    }

}
