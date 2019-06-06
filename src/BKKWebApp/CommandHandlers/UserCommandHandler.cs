using BKKWebApp.Data.Commands;
using BKKWebApp.Data.Events;
using BKKWebApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BKKWebApp.Handlers
{
    public class UserCommandHandler : HandleCommand<CreateUser>
    {
        private readonly UserRepository _repository;

        public UserCommandHandler(UserRepository repository)
        {
            _repository = repository;
        }

        public void Handle(CreateUser command)
        {
            var @event = new UserCreatedEvent(Guid.NewGuid(), -1, command.UserName, command.UserId);
            _repository.Handle(@event);
        }
    }
}
