using System;
using BKKWebApp.Data.Base;

namespace BKKWebApp.Data.Events
{
    public class UserCreatedEvent : Event
    {
		public string UserName { get; set; }
		public string UserId { get; set; }

        public UserCreatedEvent() { }
        public UserCreatedEvent(Guid aggregateId, int version, string userName, string userId) : base(aggregateId, version)
        {
			UserName = userName;
			UserId = userId;
        }
    }
}
