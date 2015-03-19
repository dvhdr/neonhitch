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
        public static NormalisedContentSet FindContent(ContentQuery query)
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

            return new NormalisedContentSet
            {
                NormalisedResults = contentResults,
                Song = new Song
                {
                    ArtistName = hitchSong.Artist,
                    SongName = hitchSong.Title,
                    SongUrl = hitchSong.AudioLink,
                }
            };
        }
    }

    public class NormalisedContentSet
    {
        public IEnumerable<NormalisedResult> NormalisedResults { get; set; }
 
        public Song Song { get; set; }

        public ContentResult Evaluate()
        {
            var normalisedResultList = NormalisedResults.ToList();

            var skipCounter = 0;
            var positionCounter = 0;
            while (positionCounter < normalisedResultList.Count())
            {
                var image = normalisedResultList.Where(x => x.Type == ContentType.Image).Skip(skipCounter).FirstOrDefault();
                if (image != null)
                {
                    image.Position = positionCounter;
                    positionCounter++;
                }


                var text = normalisedResultList.Where(x => x.Type == ContentType.Text).Skip(skipCounter).FirstOrDefault();
                if (text != null)
                {
                    text.Position = positionCounter;
                    positionCounter++;
                }

                var video = normalisedResultList.Where(x => x.Type == ContentType.Video).Skip(skipCounter).FirstOrDefault();
                if (video != null)
                {
                    video.Position = positionCounter;
                    positionCounter++;
                }

                skipCounter++;
            }

            var orderedResults = normalisedResultList.OrderBy(x => x.Position);
            var contentItems = orderedResults.Select(normalisedResult => normalisedResult.Evaluate()).ToList();

            return new ContentResult
            {
                Results = contentItems.ToArray(),
                ResultCount = contentItems.Count,
                Song = Song,
            };
        } 
    }
}
