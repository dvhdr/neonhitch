using System.Collections.Generic;
using NeonHitchContentService.EntityModel.ExternalModels.NeonHitch;

namespace NeonHitchContentService.Api
{
    public class NeonHitch : IApiSource
    {
        private const string _rootUrl = "https://neonhitch.azure-api.net/";

        public string RootUrl
        {
            get { return _rootUrl; }
        }

        public IEnumerable<RequestHeader> Headers
        {
            get { return new List<RequestHeader>(); }
        }

        public string GenerateUrl(string query)
        {
            return _rootUrl + query + "?subscription-key=b0a1653d8ebc49079a20856c6121d095";
        }

        public NeonSongResult RunImport(int songId)
        {
            // Find the song in the Neon Hitch API
            return QueryRunner.Run<NeonHitch, NeonSongResult>("Snatch/choon/" + songId);
        } 
    }
}
