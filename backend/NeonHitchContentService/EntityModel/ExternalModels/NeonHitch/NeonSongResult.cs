using System.Collections.Generic;
using NeonHitchContentService.EntityModel.QueryResults;
using Newtonsoft.Json;

namespace NeonHitchContentService.EntityModel.ExternalModels.NeonHitch
{
    public class NeonSongResult : INormalisable
    {
        public NeonSongResult()
        {
            Artist = "Neon Hitch";
        }

        public string Artist { get; set; }

        [JsonProperty(PropertyName = "ForThisArtist")]
        public string RelatedArtist { get; set; }

        public string Title { get; set; }

        [JsonProperty(PropertyName = "Uri")]
        public string AudioLink { get; set; }

        public string Bevey { get; set; }

        public string Grub { get; set; }

        public string Hood { get; set; }

        public string Mood { get; set; }

        [JsonProperty(PropertyName = "Id")]
        public string SongId { get; set; }

        public IEnumerable<NormalisedResult> Normalise()
        {
            // TODO: should porbably use reflection here
            var resultSet = new List<NormalisedResult>
            {
                new NormalisedResult
                {
                    Artist = Artist,
                    Key = "Bevey",
                    Type = ContentType.Text,
                    Value = Bevey
                },
                new NormalisedResult
                {
                    Artist = Artist,
                    Key = "Grub",
                    Type = ContentType.Text,
                    Value = Grub,
                },
                new NormalisedResult
                {
                    Artist = Artist,
                    Key = "Hood",
                    Type = ContentType.Text,
                    Value = Hood
                },
                new NormalisedResult
                {
                    Artist = Artist,
                    Key = "Mood",
                    Type = ContentType.Text,
                    Value = Mood,
                }
            };

            return resultSet;
        }
    }
}
