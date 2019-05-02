using BKKWebApp.Handlers;
using BKKWebApp.Data;
using BKKWebApp.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BKKWebApp.EventBus
{
    public class EventBus
    {
        Dictionary<Type, List<object>> eventHandlers = new Dictionary<Type, List<object>>();

        public void AddEventHandler<T_Event>(HandleEvent<T_Event> eventHandler) where T_Event : Event
        {
            if (!eventHandlers.ContainsKey(typeof(T_Event)))
            {
                eventHandlers.Add(typeof(T_Event), new List<object>());
            }

            List<object> eventHandlerList = eventHandlers[typeof(T_Event)];
            eventHandlerList.Add(eventHandler);
        }

        public void ApplyEvent<T_Event>(T_Event @event) where T_Event : Event
        {
            foreach (var handler in eventHandlers[typeof(T_Event)])
            {
                ((HandleEvent<T_Event>)handler).Handle(@event);
            }
        }

        public void DiscoverHandlers(object o)
        {
            var iss = o.GetType().GetInterfaces().ToList();
            var interfaces = iss.Where(i => typeof(HandleEvent).IsAssignableFrom(i) && i.IsGenericType).ToList();
            foreach (var interf in interfaces)
            {
                var eventType = interf.GenericTypeArguments[0];
                var method = GetType().GetMethod(nameof(AddEventHandler)).MakeGenericMethod(eventType);
                method.Invoke(this, new object[] { o });
            }
        }
    }
}
