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
        public string SongId { get; private set; }

        public ContentQuery(string songId)
        {
            SongId = songId;
        }

        public ContentResult Run()
        {
            var normalisedResults = ContentFinder.FindContent(this);

            return normalisedResults.Evaluate();
        }
    }
}
