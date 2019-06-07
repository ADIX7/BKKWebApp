using BKKWebApp.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BKKWebApp.Handlers
{
    public interface IHandleEvent { }

    public interface IHandleEvent<T_Event> : IHandleEvent where T_Event : Event
    {
        void Handle(T_Event @event);
    }

    public interface IHandleCommand { }

    public interface IHandleCommand<T_Command> : IHandleCommand where T_Command : Command
    {
        void Handle(T_Command command);
    }
}
