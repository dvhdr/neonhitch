using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NeonHitchContentService.EntityModel.QueryObjects;
using NeonHitchContentService.EntityModel.QueryResults;

namespace NeonHitchContentApiV2.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public ContentResult Get(string songId)
        {
            var qo = new ContentQuery(songId);

            var results = qo.Run();

            return results;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}