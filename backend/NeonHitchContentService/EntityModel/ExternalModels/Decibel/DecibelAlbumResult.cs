using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeonHitchContentService.EntityModel.ExternalModels.Decibel
{
    public class DecibelAlbumResultSet
    {
        public DecibelAlbumResult[] Results { get; set; }
    }

    public class DecibelAlbumResult : INormalisable
    {
        public IEnumerable<NormalisedResult> Normalise()
        {
            throw new NotImplementedException();
        }
    }
}
