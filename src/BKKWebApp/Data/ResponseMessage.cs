using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BKKWebApp.Data
{
    public enum Status
    {
        Unknown = 0,
        Successed = 1,
        Failed = 2
    }

    public class ResponseMessage
    {
        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("payload")]
        public JObject Payload { get; set; }
    }
}
