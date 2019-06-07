using BKKWebApp.Data;
using BKKWebApp.Data.Commands;
using BKKWebApp.Data.Events;
using BKKWebApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BKKWebApp.Handlers
{
    public class UserCommandHandler : IHandleCommand<CreateUser>
    {
        private readonly UserRepository _repository;
        private readonly EventManager _eventStore;

        public UserCommandHandler(UserRepository repository, EventManager eventStore)
        {
            _repository = repository;
            _eventStore = eventStore;
        }

        public void Handle(CreateUser command)
        {
            var @event = new UserCreatedEvent(Guid.NewGuid(), -1, command.UserName, command.UserId);
            _eventStore.RecordEvent(@event);
        }
    }
}
