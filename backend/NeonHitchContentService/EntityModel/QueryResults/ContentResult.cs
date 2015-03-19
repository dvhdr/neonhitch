namespace NeonHitchContentService.EntityModel.QueryResults
{
    /// <summary>
    /// A result containing content items
    /// </summary>
    public class ContentResult
    {
        /// <summary>
        /// The song that this content relates to
        /// </summary>
        public Song Song { get; set; }

        /// <summary>
        /// The number of content items found
        /// </summary>
        public int ResultCount { get; set; }

        /// <summary>
        /// A collection of content itmes
        /// </summary>
        public Content[] Results { get; set; }
    }

    /// <summary>
    /// A item of content
    /// </summary>
    public class Content
    {
        /// <summary>
        /// The type of content, eg image, text etc
        /// </summary>
        public ContentType Type { get; set; }
    
        /// <summary>
        /// The key of the content's key value pair. Only applicable for some content types
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// The value of content's key value pair.
        /// </summary>
        public string Value { get; set; }
    }

    /// <summary>
    /// The type of content
    /// </summary>
    public enum ContentType
    {
        /// <summary>
        /// Image content, provided as a link to the original image
        /// </summary>
        Image,

        /// <summary>
        /// Text content
        /// </summary>
        Text,

        /// <summary>
        /// Video content, provided as a link to the original video
        /// </summary>
        Video,
    }
}