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
                    "https://scontent.xx.fbcdn.net/hphotos-xaf1/v/t1.0-9/11066797_10152334267713078_7322254655310371471_n.jpg?oh=f33e081aaa5e570896b014408aafaf65&oe=55B4D90C"),
                new SocialMediaItem(SocialMediaType.Facebook, 
                    "https://fbcdn-sphotos-f-a.akamaihd.net/hphotos-ak-xpf1/v/t1.0-9/11018847_10152329624163078_4529615268253972108_n.jpg?oh=27c94ac3e395910131896aa31304ae1c&oe=55856066&__gda__=1433413401_4e00083ef96a2f5ca11493f80b2be420"),
                new SocialMediaItem(SocialMediaType.Facebook, 
                    "https://fbcdn-sphotos-b-a.akamaihd.net/hphotos-ak-xpf1/v/t1.0-9/p320x320/11015217_10152327673253078_3192845487428539844_n.jpg?oh=353e834ff4844567df061cfd156c31df&oe=557DA46E&__gda__=1434315896_3f2ced7901d1ea1ffb824f4d67bb343d"),
                new SocialMediaItem(SocialMediaType.Facebook, 
                    "https://fbcdn-sphotos-e-a.akamaihd.net/hphotos-ak-xpt1/v/t1.0-9/p320x320/11060470_10152322032898078_5540584288469324_n.jpg?oh=524b8650b5d18ad8f62ea7d64c17bfec&oe=5576E359&__gda__=1438128255_ab7af5e6bec1fb65c30617bbbafb23eb"),
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
