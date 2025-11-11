using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace RoadStatusLibrary.ApiClient
{
    public class RoadStatusApiClient: IRoadStatusApiClient
    {
        private readonly RoadStatusApiConfig _config;
        private readonly HttpClient _httpClient;
        private readonly ILogger<RoadStatusApiClient> _logger;
        public RoadStatusApiClient(RoadStatusApiConfig  config, HttpClient  httpClient, ILogger<RoadStatusApiClient> logger) 
        { 
            _config = config;
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<RoadStatusApiResponse>?> GetRoadStatus(string roadName)
        {
            try
            {
                var httpResponse = await _httpClient.GetAsync($"Road/{roadName}?app_key={_config.ApiKey}");
                if (!httpResponse.IsSuccessStatusCode)
                {
                    if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        return null;
                    }
                    httpResponse.EnsureSuccessStatusCode();
                }
                var response = await httpResponse.Content.ReadFromJsonAsync<List<RoadStatusApiResponse>>();
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex,"Error connecting to API");
                throw;
            }
        }
    }
}
