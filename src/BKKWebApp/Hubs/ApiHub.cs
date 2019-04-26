using BKKWebApp.Data;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BKKWebApp.Hubs
{
    public static class Extensions
    {
        public static string AsJObject(this ResponseMessage message)
        {
            return JsonConvert.SerializeObject(message);
        }
    }

    public partial class ApiHub : Hub, IApiHubServerFunctions
    {
        public async Task GetArrivalsAndDeparturesForLocation(float lat, float lng)
        {
            string latS = lat.ToString().Replace(",", ".");
            string lngS = lng.ToString().Replace(",", ".");

            var functionName = "arrivals-and-departures-for-location.json";
            var includeReferences = true;

            var query = GetBaseQuery(functionName, includeReferences) + $"lon={lngS}&lat={latS}&radius=100&onlyDepartures=false&limit=30&minutesBefore=30&minutesAfter=30&groupLimit=3&clientLon={lngS}&clientLat={latS}";

            var ret = await client.GetAsync(query);
            var content = await ret.Content.ReadAsStringAsync();
            var data =  CreateResponse(content).AsJObject();

            await Clients.All.SendAsync(nameof(GetArrivalsAndDeparturesForLocation).Substring(3), data);
        }
        public async Task GetArrivalsAndDeparturesForStop(string stopId)
        {
            var functionName = "arrivals-and-departures-for-stop.json";
            var includeReferences = true;

            var query = GetBaseQuery(functionName, includeReferences) + $"stopId={stopId}&limit=20&minutesBefore=0&minutesAfter=90&onlyDepartures=false";

            var ret = await client.GetAsync(query);
            var content = await ret.Content.ReadAsStringAsync();
            var data =  CreateResponse(content).AsJObject();

            await Clients.All.SendAsync(nameof(GetArrivalsAndDeparturesForStop).Substring(3), data);
        }

        public async Task GetStopsForLocation(float lat, float lng)
        {
            string latS = lat.ToString().Replace(",", ".");
            string lngS = lng.ToString().Replace(",", ".");

            var functionName = "stops-for-location.json";
            var includeReferences = true;

            var query = GetBaseQuery(functionName, includeReferences) + $"lon={lngS}&lat={latS}&radius=300";

            var ret = await client.GetAsync(query);
            var content = await ret.Content.ReadAsStringAsync();
            var data = CreateResponse(content).AsJObject();

            await Clients.All.SendAsync(nameof(GetStopsForLocation).Substring(3), data);
        }
    }
}