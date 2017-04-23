using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetEscapades.Nasa
{
    public partial interface INasaImageClient
    {
        /// <summary>
        /// Search for images on NASA
        /// </summary>
        /// <param name="query">Free text search terms to compare to all indexed metadata.</param>
        /// <returns></returns>
        Task<IResult<SearchResults>> Search(string query);

        /// <summary>
        /// Search for images on NASA
        /// </summary>
        /// <param name="query">Parameters defining the search</param>
        /// <returns></returns>
        Task<IResult<SearchResults>> Search(SearchQuery query);

        /// <summary>
        /// Get a list of recent assets added to NASA image database, as a
        /// </summary>
        /// <typeparam name=""></typeparam>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        Task<IResult<ICollection<string>>> GetRecentAssetIds();
    }
}
