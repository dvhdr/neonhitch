using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeonHitchContentService.EntityModel.ExternalModels
{
    public interface INormalisable
    {
        IEnumerable<NormalisedResult> Normalise();
    }
}
