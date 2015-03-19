using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeonHitchContentService.EntityModel.QueryResults;

namespace NeonHitchContentService.EntityModel.QueryObjects
{
    public class ContentQuery
    {
        public int SongId { get; private set; }

        public ContentQuery(int songId)
        {
            SongId = songId;
        }

        public ContentResult Run()
        {
            var normalisedResults = ContentFinder.FindContent(this);

            // TODO: filter and ordered results

            throw new NotImplementedException();
        }
    }
}
