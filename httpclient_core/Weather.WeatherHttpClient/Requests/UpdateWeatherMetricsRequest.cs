using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Weather.WeatherHttpClient.Requests
{
    public class UpdateWeatherMetricsRequest : HttpRequestMessage
    {
        public UpdateWeatherMetricsRequest(string city, DateTime date,int temparture):
            base(HttpMethod.Post, new Uri($"weather", UriKind.Relative))
        {
            var dataObject = new
            {
                date = date,
                city = city,
                temparture = temparture
            };

            base.Content = new StringContent(JsonConvert.SerializeObject(dataObject, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            }));
            base.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        }
    }
}
