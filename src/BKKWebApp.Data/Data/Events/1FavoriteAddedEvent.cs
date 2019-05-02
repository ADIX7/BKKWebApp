using BKKWebApp.Data.Base;
using System;

namespace BKKWebApp.Data.Events
{
    public class FavoriteAddedEvent : Event
    {
        public readonly string FavoriteId;
        public FavoriteAddedEvent(Guid aggregateId, int version, string favoriteId) : base(aggregateId, version)
        {
            FavoriteId = favoriteId;
        }
    }
}