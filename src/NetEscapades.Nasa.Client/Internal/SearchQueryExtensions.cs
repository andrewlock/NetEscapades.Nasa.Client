using System.Collections.Generic;

namespace NetEscapades.Nasa.Internal
{
    internal static class SearchQueryExtensions
    {
        public static IDictionary<string, string> ToDictionary(this SearchQuery query)
        {
            return new Dictionary<string, string>
            {
                {"q", query.Query },
                {"center", query.Center },
                {"description", query.Description },
                {"description_508", query.Description508 },
                {"keywords", query.Keywords },
                {"location", query.Location },
                {"media_type", query.MediaType },
                {"nasa_id", query.NasaId},
                {"photographer", query.Photographer },
                {"secondary_creator", query.SecondaryCreator },
                {"title", query.Title },
                {"year_start", query.YearStart },
                {"year_end", query.YearEnd },
            };
        }
    }
}
