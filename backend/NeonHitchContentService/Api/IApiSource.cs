using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeonHitchContentService.Api
{
    public interface IApiSource
    {
        string RootUrl { get; }

        IEnumerable<RequestHeader> Headers { get; }

        string GenerateUrl(string query);
    }

    public class RequestHeader
    {
        public string Key { get; set; }

        public string Value { get; set; }
    }
}
