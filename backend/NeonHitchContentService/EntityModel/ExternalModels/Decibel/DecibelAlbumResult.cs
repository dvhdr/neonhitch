using System;
using System.Collections.Generic;
using System.Linq;
using NeonHitchContentService.EntityModel.QueryResults;
using Newtonsoft.Json;

namespace NeonHitchContentService.EntityModel.ExternalModels.Decibel
{
    public class DecibelAlbumResultSet : INormalisable
    {
        [JsonIgnore]
        public string ArtistName { get; set; }

        public DecibelAlbumResult[] Results { get; set; }

        public IEnumerable<NormalisedResult> Normalise()
        {
            return Results.Where(x => !String.IsNullOrEmpty(x.ImageId)).Select(y => new NormalisedResult
            {
                Artist = ArtistName, Key = CreateKey(y), Value = ImageIdToUrl(y.ImageId), Type = ContentType.Image,
            }).ToList();
        }

        private static string ImageIdToUrl(string imageId)
        {
            return "https://rest.decibel.net/v3/images/" + imageId;
        }

        private static string CreateKey(DecibelAlbumResult result)
        {
            return result.Title + " by " + result.ArtistsLiteral;
        }
    }

    public class DecibelAlbumResult
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string ImageId { get; set; }

        public string ArtistsLiteral { get; set; }
    }
}
