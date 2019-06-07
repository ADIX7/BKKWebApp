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
        private readonly Dictionary<Type, List<object>> eventHandlers = new Dictionary<Type, List<object>>();

        public void AddEventHandler<T_Event>(IHandleEvent<T_Event> eventHandler) where T_Event : Event
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
                ((IHandleEvent<T_Event>)handler).Handle(@event);
            }
        }

        public void DiscoverHandlers(object handlerContainer)
        {
            var handlerInterfaces = handlerContainer
                .GetType()
                .GetInterfaces()
                .Where(i =>
                    typeof(IHandleEvent)
                        .IsAssignableFrom(i)
                    && i.IsGenericType)
                .ToList();

            foreach (var @interface in handlerInterfaces)
            {
                var eventType = @interface.GenericTypeArguments[0];
                var method = GetType().GetMethod(nameof(AddEventHandler)).MakeGenericMethod(eventType);
                method.Invoke(this, new object[] { handlerContainer });
            }
        }
    }
}
