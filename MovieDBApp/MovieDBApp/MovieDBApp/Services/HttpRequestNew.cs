using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieDBApp.Services
{
    public class HttpRequestNew : IHttpRequest
    {
        private static HttpClient client = new HttpClient();
        private readonly JsonSerializerOptions serializer = new JsonSerializerOptions();

        public HttpRequestNew()

        {
            //serializer = new JsonSerializerOptions

            //{
            //    ContractResolver = new CamelCasePropertyNamesContractResolver(),
            //    DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            //    NullValueHandling = NullValueHandling.Ignore
            //};

            //_serializerSettings.Converters.Add(new StringEnumConverter());

        }

        public async Task<TResult> GetAsync<TResult>(string uri)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(uri).ConfigureAwait(false);

            //await HandleResponse(response);

            StringBuilder serialized = new StringBuilder(await response.Content.ReadAsStringAsync());
            TResult result = await Task.Run(() => JsonSerializer.Deserialize<TResult>(serialized.ToString(), serializer));

            return result;

        }
    }
}
