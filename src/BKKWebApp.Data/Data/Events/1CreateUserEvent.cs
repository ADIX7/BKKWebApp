using BKKWebApp.Data.Base;
using System;

namespace BKKWebApp.Data.Data.Events
{
    public class CreateUserEvent : Event
    {
        public readonly string UserName;
        public readonly string UserId;

        public CreateUserEvent(Guid aggregateId, int version, string userName, string userId) : base(aggregateId, version)
        {
            UserName = userName;
            UserId = userId;
        }
    }
}
