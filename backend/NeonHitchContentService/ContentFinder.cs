using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeonHitchContentService.Api;
using NeonHitchContentService.EntityModel;
using NeonHitchContentService.EntityModel.QueryObjects;
using NeonHitchContentService.EntityModel.QueryResults;

namespace NeonHitchContentService
{
    public static class ContentFinder
    {
        /// <summary>
        /// Find content for the given NeonHitch song id
        /// </summary>
        /// <param name="songId"></param>
        /// <returns></returns>
        public static IEnumerable<NormalisedResult> FindContent(ContentQuery query)
        {
            var contentResults = new List<NormalisedResult>();

            // Find the song in the Heon Hitch API
            var hitchSong = new NeonHitch().RunImport(query.SongId);
            if (hitchSong == null) throw new Exception("No Neon Hitch song could be found with this Id");
            // Add content from the song
            contentResults.AddRange(hitchSong.Normalise());

            var decibel = new Decibel();
            // Get social media items
            // TODO: import content from social media apis
            //var socialMediaLinks = decibel.GetSocialMediaLinks(hitchSong.Artist);
            //foreach (var socialMediaLink in socialMediaLinks)
            //    contentResults.AddRange(socialMediaLink.ImportContent());

            // Get decibel artwork for this artist
            contentResults.AddRange(decibel.ImportArtwork(hitchSong.Artist));

            // Get decibel artwork for related artists
            contentResults.AddRange(decibel.ImportArtwork(hitchSong.RelatedArtist));

            return contentResults;
        }
    }
}
