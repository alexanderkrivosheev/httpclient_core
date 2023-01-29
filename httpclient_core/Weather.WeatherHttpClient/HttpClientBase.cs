using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Weather.WeatherHttpClient
{
    public abstract class HttpClientBase
    {
        protected class HttpClientResponce<TContent>
        {
            public bool IsSuccess { get; set; } = true;
            public TContent? Content { get; set; }
        }

        private HttpClient _client { get; }
        protected ILogger Log { get; }

        public HttpClientBase(HttpClient client, ILogger log)
        {
            _client = client;
            Log = log;
        }

        protected async Task<HttpClientResponce<TResponce>> SendAsync<TResponce>(HttpRequestMessage request)
        {
            HttpClientResponce<TResponce> result = new HttpClientResponce<TResponce>();

            try
            {
                HttpResponseMessage responseMessage = await _client.SendAsync(request);
                string responseBodyAsText = await responseMessage.Content.ReadAsStringAsync();

                if (responseMessage.IsSuccessStatusCode)
                {
                    result.Content = ParseResponse<TResponce>(responseBodyAsText);
                }
                else
                {
                    Log.LogWarning($"Response is not successful. #request: {responseMessage.RequestMessage}. #response: {responseBodyAsText}");
                }
            }
            catch (Exception exception)
            {
                result.IsSuccess = false;
                Log.LogError($"Error ocurred while performing the #request: {request}. #message: {exception.Message})");
            }

            return result;
        }

        protected T? ParseResponse<T>(string text)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(text);
            }
            catch (Exception exception)
            {
                Log.LogError($"Error ocurred while deserialization \"{text}\" to {typeof(T)} object. #message: {exception.Message}");
                return default;
            }
        }
    }
}
