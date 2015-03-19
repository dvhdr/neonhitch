namespace NeonHitchContentService.EntityModel.QueryResults
{
    /// <summary>
    /// A result containing songs
    /// </summary>
    public class SongResult
    {
        /// <summary>
        /// A collection of songs
        /// </summary>
        public Song[] ResultSet { get; set; }
    }

    public class Song
    {
        /// <summary>
        /// The name of the song this content relates to
        /// </summary>
        public string SongName { get; set; }

        /// <summary>
        /// The name of the artist this content relates to
        /// </summary>
        public string ArtistName { get; set; }

        /// <summary>
        /// The name of the album this content relates to
        /// </summary>
        public string AlbumName { get; set; }

        /// <summary>
        /// A link to a audio file of the song
        /// </summary>
        public string SongUrl { get; set; }
    }
}