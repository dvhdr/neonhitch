using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeonHitchContentService.EntityModel.QueryResults;

namespace NeonHitchContentService.EntityModel.ExternalModels.NeonHitch
{
    public class NeonImageResult : INormalisable
    {
        public string Id { get; set; }

        public string Uri { get; set; }

        public string Title { get; set; }

        public IEnumerable<NormalisedResult> Normalise()
        {
            return new List<NormalisedResult>()
            {
                new NormalisedResult
                {
                    Artist = "Neon Hitch",
                    Key = "Neon Hitch API Image",
                    Type = ContentType.Image,
                    Value = Uri
                }
            };
        }
    }
}
