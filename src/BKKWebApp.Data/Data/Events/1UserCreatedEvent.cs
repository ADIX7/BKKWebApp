using BKKWebApp.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BKKWebApp.Data.Events
{
    public class UserAddedEvent : Event
    {
        string UserName;
        public UserAddedEvent(Guid aggregateId, int version) : base(aggregateId, version)
        {
        }
    }
}
