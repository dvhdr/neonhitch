using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeonHitchContentService.EntityModel;

namespace NeonHitchContentService.Api
{
    public class Twitter : IApiSource
    {
        public string RootUrl
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<RequestHeader> Headers
        {
            get { throw new NotImplementedException(); }
        }

        public string GenerateUrl(string query)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NormalisedResult> ImportPhotos(string address)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NormalisedResult> ImportTweets(string address)
        {
            throw new NotImplementedException();
        }

        private static string PageNameFromAddress(string address)
        {
            return address.Replace("https://twitter.com/", String.Empty);
        } 
    }
}
