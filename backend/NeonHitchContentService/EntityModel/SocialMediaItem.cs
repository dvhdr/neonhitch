using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeonHitchContentService.Api;
using NeonHitchContentService.EntityModel.ExternalModels;
using NeonHitchContentService.EntityModel.QueryResults;

namespace NeonHitchContentService.EntityModel
{
    public class SocialMediaItem : INormalisable
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

        public IEnumerable<NormalisedResult> Normalise()
        {
            return new List<NormalisedResult>
            {
                new NormalisedResult
                {
                    Artist = "Neon Hitch",
                    Type = ContentType.Image,
                    Value = Address,
                    Key = Type.ToString(),
                }
            };
        }
    }

    public static class SocialMediaHelper
    {
        public static IEnumerable<SocialMediaItem> Get()
        {
            return new List<SocialMediaItem>
            {
                new SocialMediaItem(SocialMediaType.Facebook,
                    "https://scontent.xx.fbcdn.net/hphotos-xfp1/v/t1.0-9/13055_10152336331508078_1539626426081957808_n.jpg?oh=1bfc2b7dd398da2eaa1dc60ca49b96ca&oe=557A5C12"),
                new SocialMediaItem(SocialMediaType.Facebook,
                    "https://scontent.xx.fbcdn.net/hphotos-xaf1/v/t1.0-9/11066797_10152334267713078_7322254655310371471_n.jpg?oh=f33e081aaa5e570896b014408aafaf65&oe=55B4D90C")
            };
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
