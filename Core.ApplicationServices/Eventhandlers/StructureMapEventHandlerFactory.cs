using Core.Shared.EventHandlers;
using Core.Shared.Events;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ApplicationServices.Eventhandlers
{
    public class StructureMapEventHandlerFactory : IEventHandlerFactory
    {

        private IContainer container;

        public StructureMapEventHandlerFactory(IContainer container)
        {
            this.container = container;

            var a = container.WhatDoIHave();

            var b = 1;

        }

        public IEnumerable<IEventHandler<T>> GetHandlers<T>() where T : Event
        {
            //var handlers = GetHandlerType<T>();
            //var lstHandlers = handlers.Select(handler => (IEventHandler<T>)container.GetInstance(handler)).ToList();

            List<IEventHandler<T>> handlers = new List<IEventHandler<T>>();

            var evtHandlers = container
                .GetAllInstances(typeof(IEventHandler<T>));

            foreach (var handler in evtHandlers)
                handlers.Add(handler as IEventHandler<T>);

            return handlers;
        }

        private static IEnumerable<Type> GetHandlerType<T>() where T : Event
        {

            var handlers = typeof(IEventHandler<>).Assembly.GetExportedTypes()
                .Where(x => x.GetInterfaces()
                    .Any(a => a.IsGenericType && a.GetGenericTypeDefinition() == typeof(IEventHandler<>))).Where(h => h.GetInterfaces().Any(ii => ii.GetGenericArguments().Any(aa => aa == typeof(T)))).ToList();


            return handlers;
        }
    }
}
