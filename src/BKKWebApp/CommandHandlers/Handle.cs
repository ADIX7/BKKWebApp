using BKKWebApp.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BKKWebApp.Handlers
{
    public interface HandleEvent { }
    public interface HandleEvent<T_Event> : HandleEvent where T_Event : Event
    {
        void Handle(T_Event @event);
    }

    public interface HandleCommand { }
    public interface HandleCommand<T_Command> : HandleCommand where T_Command : Command
    {
        void Handle(T_Command command);
    }
}
