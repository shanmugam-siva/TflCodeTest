using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadStatusLibrary.ApiClient
{
    public interface IRoadStatusApiClient
    {
        public  Task<List<RoadStatusApiResponse>?> GetRoadStatus(string  roadName);
    }
}
