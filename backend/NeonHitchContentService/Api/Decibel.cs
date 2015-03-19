using System.Collections.Generic;
using System.Linq;
using NeonHitchContentService.EntityModel;
using NeonHitchContentService.EntityModel.ExternalModels.Decibel;

namespace NeonHitchContentService.Api
{
    class Decibel : IApiSource
    {
        private const string _rootUrl = "https://rest.decibel.net/v3/";

        public string RootUrl
        {
            get { return _rootUrl; }
        }

        public IEnumerable<RequestHeader> Headers
        {
            get
            {
                return new List<RequestHeader>
                {
                    new RequestHeader {Key = "DecibelAppId", Value = "5589c9d1"},
                    new RequestHeader {Key = "DecibelAppKey", Value = "4154f53630c5cffd106cbe3ba0bd1eff"},
                };
            }
        }

        public string GenerateUrl(string query)
        {
            return _rootUrl + query;
        }

        public IEnumerable<SocialMediaItem> GetSocialMediaLinks(string artistName)
        {
            var result = QueryRunner.Run<Decibel, DecibelArtistResult>("artists?name=" + artistName + "&depth=urls");

            var links = new List<SocialMediaItem>();
            foreach (var decibelArtist in result.Results)
                links.AddRange(
                    decibelArtist.WebAddresses.Select(
                        x => new SocialMediaItem(SiteNameToSocialMediaType(x), x.Address)));

            return links;
        }

        public IEnumerable<NormalisedResult> ImportArtwork(string artistName)
        {
            var results = QueryRunner.Run<Decibel, DecibelAlbumResultSet>("albums?artists=" + artistName);
            results.ArtistName = artistName;

            return results.Normalise();
        } 

        private static SocialMediaType SiteNameToSocialMediaType(DecibelWebAddress item)
        {
            // First, trying getting type from website name
            SocialMediaType? type;
            SiteNameToSocialMediaTypeDict.TryGetValue(item.Website, out type);
            if (type != null) return (SocialMediaType)type;

            // Otherwise, trying using the url
            foreach (var socialMediaType in SiteNameToSocialMediaTypeDict)
            {
                if (item.Address.ToLower().Contains(socialMediaType.Key.ToLower()) && socialMediaType.Value != null)
                    return (SocialMediaType)socialMediaType.Value;
            }

            return SocialMediaType.Unknown;
        }

        private static readonly Dictionary<string, SocialMediaType?> SiteNameToSocialMediaTypeDict = new Dictionary<string, SocialMediaType?>
        {
            {"youtube", SocialMediaType.Youtube},
            {"facebook", SocialMediaType.Facebook},
            {"myspace", SocialMediaType.MySpace},
            {"twitter", SocialMediaType.Twitter},
            {"soundcloud", SocialMediaType.Soundcloud}
        };
    }
}
