using BKKWebApp.Data;
using BKKWebApp.Data.Commands;
using BKKWebApp.Data.Events;
using BKKWebApp.Data.Misc;
using BKKWebApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BKKWebApp.Handlers
{
    public class UserCommandHandler : IHandleCommand<CreateUser>, IHandleCommand<AddFavorite>
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

        public void Handle(AddFavorite command)
        {
            var user = _repository.Users.First(u => u.UserId == command.UserId);
            FavoriteAddedEvent @event;
            var counter = 0;
            do
            {
                @event = new FavoriteAddedEvent(user.Id, user.Version + 1, command.UserId, command.Favorite);
            }
            while (!_eventStore.RecordEvent(@event) && counter++ < MiscData.MaxEventRetry);
        }
    }
}
