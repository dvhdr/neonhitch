using System.Collections.Generic;
using Newtonsoft.Json;

namespace NeonHitchContentService.EntityModel.ExternalModels
{
    public class FacebookPhotoUploaded
    {
        [JsonProperty(PropertyName = "Data")]
        public FacebookPhoto[] Photos { get; set; }
    }

    public class FacebookPhoto : INormalisable
    {
        public string Source { get; set; }

        public IEnumerable<NormalisedResult> Normalise()
        {
            throw new System.NotImplementedException();
        }
    }
}
