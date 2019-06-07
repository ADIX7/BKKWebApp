/* using System;
using BKKWebApp.Data.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BKKWebApp.Data.Events1
{
    public class UserCreatedEvent : Event
    {
        public const string eventName = "userCreated";

		public string UserName { get; set; }
		public string UserId { get; set; }

        internal UserCreatedEvent(string source) : base(eventName)
        {
            var sourceObj = JObject.Parse(source);
            throw new NotImplementedException();
            //sourceObj[nameof(UserName)].
        }

        public UserCreatedEvent(Guid aggregateId, int version, string userName, string userId) : base(aggregateId, version, eventName)
        {
			UserName = userName;
			UserId = userId;
        }

        public override string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
 */