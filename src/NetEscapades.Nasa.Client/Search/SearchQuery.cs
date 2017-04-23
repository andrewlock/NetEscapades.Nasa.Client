namespace NetEscapades.Nasa
{
    public class SearchQuery
    {
        /// <summary>
        /// Free text search terms to compare to all indexed metadata.
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// NASA center which published the media.
        /// </summary>
        public string Center { get; set; }

        /// <summary>
        /// Terms to search for in “Description” fields.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Terms to search for in “508 Description” fields.
        /// </summary>
        public string Description508 { get; set; }

        /// <summary>
        /// Terms to search for in “Keywords” fields. Separate multiple values with commas.
        /// </summary>
        public string Keywords { get; set; }

        /// <summary>
        /// Terms to search for in “Location” fields.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Media types to restrict the search to. Available types: [“image”, “audio”]. Separate multiple values withcommas.
        /// </summary>
        public string MediaType { get; set; }

        /// <summary>
        /// The media asset’s NASA ID.
        /// </summary>
        public string NasaId { get; set; }

        /// <summary>
        /// The primary photographer’s name.
        /// </summary>
        public string Photographer { get; set; }

        /// <summary>
        /// A secondary photographer/videographer’s name.
        /// </summary>
        public string SecondaryCreator { get; set; }

        /// <summary>
        /// Terms to search for in “Title” fields.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The start year for results. Format: YYYY.
        /// </summary>
        public string YearStart { get; set; }

        /// <summary>
        /// The end year for results. Format: YYYY.
        /// </summary>
        public string YearEnd { get; set; }
    }
}
