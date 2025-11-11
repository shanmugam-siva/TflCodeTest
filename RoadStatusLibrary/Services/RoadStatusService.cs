using RoadStatusLibrary.ApiClient;

namespace RoadStatusLibrary.Services
{
    public class RoadStatusService : IRoadStatusService
    {
        private IRoadStatusApiClient _roadStatusAPIClient;

        public RoadStatusService(IRoadStatusApiClient roadStatusAPIClient)
        {
            _roadStatusAPIClient = roadStatusAPIClient;
        }

        public async Task<List<string>> GetRoadStatus(string roadName)
        {
            var apiResponse =  await _roadStatusAPIClient.GetRoadStatus(roadName);
            var result = new List<string>();

            //Get the first object from the list  matching  the road id 
            var roadData = apiResponse?.FirstOrDefault(x => string.Equals(x.Id, roadName, StringComparison.InvariantCultureIgnoreCase));
            if (roadData == null)
            {
                result.Add($"{roadName} is not a valid road");
            }
            else
            {
                result.Add($"The status of the {roadData.DisplayName} is as follows");
                result.Add($"Road Status is {roadData.StatusSeverity}");
                result.Add($"Road Status Description is {roadData.StatusSeverityDescription}");
            }
            return result;
        }
    }
}
