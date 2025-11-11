using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadStatusLibrary.Services
{
    public interface IRoadStatusService
    {
        Task<List<string>> GetRoadStatus(string roadName);
    }
}
