using System;

namespace BKKWebApp.Data.Base
{
    public abstract class Event
    {
        public Guid AggregateId { get; }
        public int Version { get; }
        public string Type { get; }

        protected Event(string type) => Type = type ?? throw new ArgumentNullException(nameof(type));

        protected Event(Guid aggregateId, int version, string type)
        {
            AggregateId = aggregateId;
            Version = version;
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        public abstract string Serialize();
    }
}
