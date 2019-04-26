using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BKKWebApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BKKWebApp.Controllers
{
    public static class Extensions
    {
        public static string AsJObject(this ResponseMessage message)
        {
            return JsonConvert.SerializeObject(message);
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class BkkAPIController : ControllerBase
    {
        public const string ArrivalsAndDeparturesForLocation = "arrivals-and-departures-for-location";
        HttpClient client = new HttpClient();

        string GetBaseQuery(string functionName, bool includeReferences)
        {
            var baseUrl = "http://futar.bkk.hu/bkk-utvonaltervezo-api/ws/otp/api/where/";
            var key = "";
            var version = "3";
            var appVersion = "";

            return baseUrl + functionName + $"?key={key}&version={version}&appVersion={appVersion}&includeReferences={includeReferences.ToString().ToLower()}&";
        }

        public ResponseMessage CreateResponse(string content)
        {
            var parsedContent = JObject.Parse(content);

            var response = new ResponseMessage()
            {
                Status = Status.Succeeded,
                Payload = parsedContent
            };

            return response;
        }

        [HttpGet(ArrivalsAndDeparturesForLocation)]
        public async Task<ActionResult<string>> GetArrivalsAndDeparturesForLocation([FromQuery]float lat, [FromQuery]float lng)
        {
            string latS = lat.ToString().Replace(",", ".");
            string lngS = lng.ToString().Replace(",", ".");

            var functionName = ArrivalsAndDeparturesForLocation + ".json";
            var includeReferences = true;

            var query = GetBaseQuery(functionName, includeReferences) + $"lon={lngS}&lat={latS}&radius=100&onlyDepartures=false&limit=30&minutesBefore=30&minutesAfter=30&groupLimit=3&clientLon={lngS}&clientLat={latS}";

            var ret = await client.GetAsync(query);
            var content = await ret.Content.ReadAsStringAsync();
            return CreateResponse(content).AsJObject();
        }

        [HttpGet("stops-for-location")]
        public async Task<ActionResult<string>> GetStopsForLocation([FromQuery]float lat, [FromQuery]float lng)
        {
            string latS = lat.ToString().Replace(",", ".");
            string lngS = lng.ToString().Replace(",", ".");

            var functionName = "stops-for-location.json";
            var includeReferences = true;

            var query = GetBaseQuery(functionName, includeReferences) + $"lon={lngS}&lat={latS}&radius=300";

            var ret = await client.GetAsync(query);
            var content = await ret.Content.ReadAsStringAsync();
            return CreateResponse(content).AsJObject();
        }

        [HttpGet("arrivals-and-departures-for-stop")]
        public async Task<ActionResult<string>> GetArrivalsAndDeparturesForStop([FromQuery]string stopId)
        {
            var functionName = "arrivals-and-departures-for-stop.json";
            var includeReferences = true;

            var query = GetBaseQuery(functionName, includeReferences) + $"stopId={stopId}&limit=20&minutesBefore=0&minutesAfter=90&onlyDepartures=false";

            var ret = await client.GetAsync(query);
            var content = await ret.Content.ReadAsStringAsync();
            return CreateResponse(content).AsJObject();
        }
    }
}