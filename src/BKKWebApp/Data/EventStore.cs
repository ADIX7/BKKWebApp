using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using BKKWebApp.Data.Base;

namespace BKKWebApp.Data
{
    public class EventManager
    {
        private readonly EventStoreDbContext _eventStoreDb;
        private readonly EventBus.EventBus _eventBus;
        private readonly ConcurrentQueue<EventDescriptor> _unStoredEvents = new ConcurrentQueue<EventDescriptor>();
        private readonly Dictionary<Guid, int> _versions = new Dictionary<Guid, int>();

        public EventManager(EventStoreDbContext eventStoreDb, EventBus.EventBus eventBus)
        {
            _eventStoreDb = eventStoreDb;
            _eventBus = eventBus;
        }

        public bool RecordEvent(Event @event)
        {
            var version = _versions[@event.AggregateId];
            if (@event.Version != version + 1) return false;

            var eventDescriptor = new EventDescriptor() { AggregateId = @event.AggregateId, Recorded = DateTime.Now, Event = @event.ToString() };

            _unStoredEvents.Enqueue(eventDescriptor);
            _eventBus.ApplyEvent(@event);

            //TODO schedule
            StoreEvents();

            return true;
        }

        public void StoreEvents()
        {
            while (!_unStoredEvents.IsEmpty)
            {
                var success = _unStoredEvents.TryDequeue(out var unStoredEventDescriptor);
                if (success)
                {
                    _eventStoreDb.Events.Add(unStoredEventDescriptor);
                }
            }
        }
    }
}