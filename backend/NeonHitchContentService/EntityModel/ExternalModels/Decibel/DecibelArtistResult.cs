using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeonHitchContentService.EntityModel.ExternalModels.Decibel
{
    public class DecibelArtistResult : INormalisable
    {
        public DecibelArtist[] Results { get; set; }

        public IEnumerable<NormalisedResult> Normalise()
        {
            throw new NotImplementedException();
        }
    }

    public class DecibelArtist
    {
        public string Id { get; set; }

        public string StageName { get; set; }

        public string Gender { get; set; }

        public DecibelWebAddress[] WebAddresses { get; set; }
    }

    public class DecibelWebAddress
    {
        public string Address { get; set; }

        public string Website { get; set; }
    }
}
