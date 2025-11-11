using Moq;
using RoadStatusLibrary.ApiClient;
using RoadStatusLibrary.Services;

namespace RoadStatusLibraryTest.Services
{
    public class RoadStatusServiceTests
    {
        [Fact]
        public async void ReturnRoadStatusReceivedFromAPI()
        {
            //Arrange
            var mockedRoadStatusApiClient = new Mock<IRoadStatusApiClient>();
            var mockAPIResponse = new List<RoadStatusApiResponse> {
               new RoadStatusApiResponse
               {
                   DisplayName = "A1",
                   StatusSeverity = "Good",
                   StatusSeverityDescription ="No Exceptional Delays",
                   Id = "a1"
               }
            };
            var roadStatusService = new RoadStatusService(mockedRoadStatusApiClient.Object);
            mockedRoadStatusApiClient.Setup(x => x.GetRoadStatus("A1")).ReturnsAsync(mockAPIResponse);
            var expectedResult = new List<string>
            {
                "The status of the A1 is as follows",
                "Road Status is Good",
                "Road Status Description is No Exceptional Delays"
            };
            //Act
            var result = await roadStatusService.GetRoadStatus("A1");

            //Assert
            mockedRoadStatusApiClient.Verify();
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async void ReturnAsInvalidRoadWhenAPIResponseIsEmpty()
        {
            //Arrange
            var mockedRoadStatusApiClient = new Mock<IRoadStatusApiClient>();
            var mockAPIResponse = new List<RoadStatusApiResponse>();
            var roadStatusService = new RoadStatusService(mockedRoadStatusApiClient.Object);
            mockedRoadStatusApiClient.Setup(x => x.GetRoadStatus("A4444")).ReturnsAsync(mockAPIResponse);
            var expectedResult = new List<string>
            {
                "A4444 is not a valid road",
            };
            //Act
            var result = await roadStatusService.GetRoadStatus("A4444");

            //Assert
            mockedRoadStatusApiClient.Verify();
            Assert.Equal(expectedResult, result);

        }

        [Fact]
        public async void ReturnAsInvalidRoadWhenAPIResponseIsNull()
        {
            //Arrange
            var mockedRoadStatusApiClient = new Mock<IRoadStatusApiClient>();
            var roadStatusService = new RoadStatusService(mockedRoadStatusApiClient.Object);
            mockedRoadStatusApiClient.Setup(x => x.GetRoadStatus("A4444")).ReturnsAsync((List<RoadStatusApiResponse>?)null);
            var expectedResult = new List<string>
            {
                "A4444 is not a valid road",
            };
            //Act
            var result = await roadStatusService.GetRoadStatus("A4444");

            //Assert
            mockedRoadStatusApiClient.Verify();
            Assert.Equal(expectedResult, result);

        }
        [Fact]
        public async void ReturnRoadStatusAsInvalidRoadIfNoRecordForTheRoadId()
        {
            //Arrange
            var mockedRoadStatusApiClient = new Mock<IRoadStatusApiClient>();
            var mockAPIResponse = new List<RoadStatusApiResponse> {
               new RoadStatusApiResponse
               {
                   DisplayName = "A11",
                   StatusSeverity = "Good",
                   StatusSeverityDescription ="No Exceptional Delays",
                   Id = "a11"
               }
            };
            var roadStatusService = new RoadStatusService(mockedRoadStatusApiClient.Object);
            mockedRoadStatusApiClient.Setup(x => x.GetRoadStatus("A1")).ReturnsAsync(mockAPIResponse);
            var expectedResult = new List<string>
            {
               "A1 is not a valid road"
            };
            //Act
            var result = await roadStatusService.GetRoadStatus("A1");

            //Assert
            mockedRoadStatusApiClient.Verify();
            Assert.Equal(expectedResult, result);
        }
    }
}