using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;

namespace Weather.WeatherHttpClient.Handlers
{
    internal class AuthRequestHandler : DelegatingHandler
    {
        private class LocalCache
        {
            private MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

            public async Task<TValue> GetOrCreateAsync<TValue>(string key, Func<Task<TValue>> func)
            {
                if (!_cache.TryGetValue(key, out TValue cacheEntry))
                {
                    cacheEntry = await func();
                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(1000));
                    _cache.Set(key, cacheEntry, cacheEntryOptions);
                }
                return cacheEntry;
            }
        }

        private readonly ILogger _logger;
        private LocalCache _cache = new LocalCache();
        public AuthRequestHandler(ILogger logger)
        {
            _logger = logger;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            string token = await GetTokenAsync(); 

            request.Headers.Authorization = new AuthenticationHeaderValue(Constants.AuthSchemeName, token);

            var response = await base.SendAsync(request, cancellationToken);

            return response;
        }

        public async Task<string> GetTokenAsync()
        {
            return await _cache.GetOrCreateAsync(Constants.WeatherAutTokenName, GenerateTokenAsync);
        }

        private async Task<string> GenerateTokenAsync()
        {
            _logger.LogInformation("Token has been expired.Generating the new token");

            return await Task.FromResult("Token");
        }
    }
}
