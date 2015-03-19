using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Web.Http;
using System.Web.Routing;
using NeonHitchContentService.EntityModel.QueryObjects;
using NeonHitchContentService.EntityModel.QueryResults;

namespace NeonHitchContentApi.Controllers
{
    public class ValuesController : ApiController
    {
        // For testing
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// Get content for the given song
        /// </summary>
        /// <param name="songId">The id of the song to get content for</param>
        /// <returns>A collection of content</returns>
        [HttpGet]
        public ContentResult GetContent(int songId)
        {
            var qo = new ContentQuery(songId);

            var results = qo.Run();

            return results;
        }

        /// <summary>
        /// Get a collection of songs that match the given search terms
        /// </summary>
        /// <param name="terms">The search terms</param>
        /// <returns></returns>
        [HttpGet]
        public SongResult Songs(string terms)
        {
            throw new NotImplementedException();
        }
    }
}