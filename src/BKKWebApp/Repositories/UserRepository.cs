using BKKWebApp.Handlers;
using BKKWebApp.Data.Events;
using BKKWebApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BKKWebApp.Repositories
{
    public class UserRepository : HandleEvent<UserCreatedEvent>
    {
        private readonly List<User> _users = new List<User>();
        private readonly EventBus.EventBus _eventBus;

        public UserRepository(EventBus.EventBus eventBus)
        {
            _eventBus = eventBus;
            _eventBus.DiscoverHandlers(this);
        }

        public void Handle(UserCreatedEvent @event)
        {
            var newUser = new User(@event.AggregateId, @event.UserId, @event.UserName);
            _users.Add(newUser);
        }
    }
}
