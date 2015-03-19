using System.Net;
using System.Text;
using NeonHitchContentService.Api;
using Newtonsoft.Json;

namespace NeonHitchContentService
{
    class QueryRunner
    {
        public static TResult Run<TDataSource, TResult>(string query)
            where TDataSource : IApiSource, new()
        {
            var dataSource = new TDataSource();

            using (var client = new WebClient())
            {
                SetRequestHeaders(client, dataSource);
                var response = client.DownloadString(dataSource.GenerateUrl(query));
                return response.Length > 0 ? Deserialise<TResult>(response) : default(TResult);
            }
        }

        private static void SetRequestHeaders(WebClient client, IApiSource source, IWebProxy proxy = null,
            Encoding encoding = null)
        {
            client.Proxy = proxy;
            client.Encoding = encoding ?? Encoding.UTF8;
            foreach (var header in source.Headers)
                client.Headers.Add(header.Key, header.Value);
        }

        private static TResult Deserialise<TResult>(string response)
        {
            return response == null ? default(TResult) : JsonConvert.DeserializeObject<TResult>(response);
        }
    }
}
