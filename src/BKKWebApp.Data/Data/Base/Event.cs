using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BKKWebApp.Data.Base
{
    public abstract class Event
    {
        public Guid AggregateId { get; set; }
        public int Version { get; set; }

        public Event() { }

        public Event(Guid aggregateId, int version)
        {
            AggregateId = aggregateId;
            Version = version;
        }
    }
}
