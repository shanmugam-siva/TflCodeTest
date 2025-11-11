using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using RichardSzalay.MockHttp;
using RoadStatusLibrary;
using RoadStatusLibrary.ApiClient;
using System.Net;

namespace RoadStatusLibraryTest.ApiClient
{
    public class RoadStatusApiClientTests
    {
        [Fact]
        public async void ReturnAPIResponseWhenResponseIsSuccessful()
        {
            //Arrange
            var mockHttp = new MockHttpMessageHandler();
            var logger = new Mock<ILogger<RoadStatusApiClient>>();
            var apiConfig = new RoadStatusApiConfig
            {
                BaseUrl = "http://localhost",
                ApiKey = "xxx"
            };
            var jsonString = "[\r\n{\r\n\"$type\": \"Tfl.Api.Presentation.Entities.RoadCorridor, Tfl.Api.Presentation.Entities\",\r\n\"id\": \"a2\",\r\n\"displayName\": \"A2\",\r\n\"statusSeverity\": \"Good\",\r\n\"statusSeverityDescription\": \"No Exceptional Delays\",\r\n\"bounds\": \"[[-0.0857,51.44091],[0.17118,51.49438]]\",\r\n\"envelope\": \"[[-0.0857,51.44091],[0.0857,51.49438],[0.17118,51.49438],[0.17118,51.44091],[0.0857,51.44091]]\",\r\n\"url\": \"/Road/a2\"\r\n}\r\n]";
            var expectedResult = new List<RoadStatusApiResponse> {
               new RoadStatusApiResponse
               {
                   DisplayName = "A2",
                   StatusSeverity = "Good",
                   StatusSeverityDescription ="No Exceptional Delays",
                   Id = "a2"
               }
            };

            var client = mockHttp.ToHttpClient();
            client.BaseAddress = new Uri(apiConfig.BaseUrl);
            mockHttp.When($"http://localhost/Road/A2?app_key={apiConfig.ApiKey}").Respond("application/json", jsonString);
            var apiClient = new RoadStatusApiClient(apiConfig, client, logger.Object);

            //Act
            var response = await apiClient.GetRoadStatus("A2");
            
            //Assert
            response.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async void ReturnNullWhenResponseIsNotFound()
        {
            //Arrange
            var mockHttp = new MockHttpMessageHandler();
            var logger = new Mock<ILogger<RoadStatusApiClient>>();
            var apiConfig = new RoadStatusApiConfig
            {
                BaseUrl = "http://localhost",
                ApiKey = "xxx"
            };
           
            var client = mockHttp.ToHttpClient();
            client.BaseAddress = new Uri(apiConfig.BaseUrl);
            mockHttp.When($"http://localhost/Road/A2?app_key={apiConfig.ApiKey}").Respond(HttpStatusCode.NotFound);
            var apiClient = new RoadStatusApiClient(apiConfig, client,  logger.Object);

            //Act
            var response = await apiClient.GetRoadStatus("A2");

            //Assert
            response.Should().BeNull();
        }
       
        [Fact]
        public async void ThrowExceptionWhenInternalServerError()
        {
            //Arrange
            var mockHttp = new MockHttpMessageHandler();
            var logger = new Mock<ILogger<RoadStatusApiClient>>();
            var apiConfig = new RoadStatusApiConfig
            {
                BaseUrl = "http://localhost",
                ApiKey = "xxx"
            };

            var client = mockHttp.ToHttpClient();
            client.BaseAddress = new Uri(apiConfig.BaseUrl);
            mockHttp.When($"http://localhost/Road/A2?app_key={apiConfig.ApiKey}").Respond(HttpStatusCode.InternalServerError);
            var apiClient = new RoadStatusApiClient(apiConfig, client, logger.Object);

            //Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(() => apiClient.GetRoadStatus("A2"));

        }

    }
}