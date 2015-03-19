using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeonHitchContentService.Api;

namespace NeonHitchContentService.EntityModel
{
    public class SocialMediaItem
    {
        public SocialMediaType Type { get; private set; }

        public string Address { get; private set; }

        public SocialMediaItem(SocialMediaType type, string address)
        {
            Type = type;
            Address = address;
        }

        public IEnumerable<NormalisedResult> ImportContent()
        {
            switch (Type)
            {
                case SocialMediaType.Facebook:
                    return new Facebook().ImportPhotos(Address);
                case SocialMediaType.Twitter:
                    var twitter = new Twitter();
                    var photos = twitter.ImportPhotos(Address);
                    var tweets = twitter.ImportTweets(Address); // TODO: why?
                    return (photos != null) ? photos.Concat(tweets) : tweets;

            }

            throw new NotImplementedException();
        } 
    }

    public enum SocialMediaType
    {
        Twitter,
        Facebook,
        Soundcloud,
        MySpace,
        Youtube,
        Unknown,
    }
}
