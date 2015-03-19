using System;
using System.Collections.Generic;
using NeonHitchContentService.EntityModel;
using NeonHitchContentService.EntityModel.ExternalModels;

namespace NeonHitchContentService.Api
{
    public class Facebook : IApiSource
    {
        private const string _rootUrl = "https://graph.facebook.com/v2.2/";

        public string RootUrl
        {
            get { return _rootUrl; }
        }

        public IEnumerable<RequestHeader> Headers
        {
            get { throw new NotImplementedException(); }
        }

        public string GenerateUrl(string query)
        {
            return _rootUrl + query;
        }

        public IEnumerable<NormalisedResult> ImportPhotos(string address)
        {
            // TODO: oauth authentication
            

            var results = QueryRunner.Run<Facebook, FacebookPhotoUploaded>(PageNameFromAddress(address) + "/photos/uploaded");

            var normalisedResults = new List<NormalisedResult>();
            foreach (var facebookPhoto in results.Photos)
                normalisedResults.AddRange(facebookPhoto.Normalise());

            return normalisedResults;
        }

        private static string PageNameFromAddress(string address)
        {
            return address.Replace("https://www.facebook.com/", String.Empty);
        }
    }
}
