using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RoadStatusLibrary;
using RoadStatusLibrary.ApiClient;
using RoadStatusLibrary.Services;

namespace RoadStatusUI
{
    public class Program
    {
        public async static Task<int> Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            //Read API base url and key from config
            var apiConfig = builder.Configuration.GetSection("RoadStatusAPI").Get<RoadStatusApiConfig>() ?? new RoadStatusApiConfig();
             
            builder.Services.AddSingleton(apiConfig);
            builder.Services.AddTransient<IRoadStatusApiClient, RoadStatusApiClient>();
            builder.Services.AddTransient<IRoadStatusService, RoadStatusService>();

            // Inject http client for road status API
            builder.Services.AddHttpClient<IRoadStatusApiClient, RoadStatusApiClient>((client) =>
            {
                client.BaseAddress = new Uri(apiConfig?.BaseUrl);
            });

            var host = builder.Build();
            var apiService = host.Services.GetService<IRoadStatusService>();
            var  logger =  host.Services.GetService<ILogger<Program>>();
            var exitCode = 1;

            try
            {
                if (!args.Any()) 
                {
                    Console.WriteLine("No road name  provided");
                    Console.WriteLine("Eg valid command : RoadStatusUI A1");
                    return exitCode;
                }
                List<string> results = await apiService.GetRoadStatus(args[0]);
               
                //Display  the road display name.  result will have atleast one element
                Console.WriteLine(results[0]);

                //Result count will  be  1 if it is an  invalid  road

                exitCode=  results.Count == 1 ? 1 : 0;

                //Display status and decription
                foreach (var result in results.Skip(1))
                {
                    Console.WriteLine($"\t{result}");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return exitCode;
            }

            return exitCode;
        }
    }
}
