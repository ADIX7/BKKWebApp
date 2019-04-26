using System.Net.Http;
using BKKWebApp.Data;
using Newtonsoft.Json.Linq;

namespace BKKWebApp.Hubs
{

    partial class ApiHub
    {
        HttpClient client = new HttpClient();

        static string GetBaseQuery(string functionName, bool includeReferences)
        {
            var baseUrl = "http://futar.bkk.hu/bkk-utvonaltervezo-api/ws/otp/api/where/";
            var key = "";
            var version = "3";
            var appVersion = "";

            return baseUrl + functionName + $"?key={key}&version={version}&appVersion={appVersion}&includeReferences={includeReferences.ToString().ToLower()}&";
        }

        static ResponseMessage CreateResponse(string content)
        {
            var parsedContent = JObject.Parse(content);

            var response = new ResponseMessage()
            {
                Status = Status.Succeeded,
                Payload = parsedContent
            };

            return response;
        }
    }
}