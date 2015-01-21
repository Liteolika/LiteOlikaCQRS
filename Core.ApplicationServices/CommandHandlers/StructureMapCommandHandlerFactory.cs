using Core.Shared.CommandHandlers;
using Core.Shared.Commands;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ApplicationServices.CommandHandlers
{
    public class StructureMapCommandHandlerFactory : ICommandHandlerFactory
    {

        private IContainer container;

        public StructureMapCommandHandlerFactory(IContainer container)
        {
            this.container = container;
        }

        public ICommandHandler<T> GetHandler<T>() where T : Command
        {
            //var handlers = GetHandlerTypes<T>().ToList();

            //var cmdHandler = handlers.Select(handler =>
            //    (ICommandHandler<T>)container.GetInstance(handler)).FirstOrDefault();

            var cmdHandler = (ICommandHandler<T>)container.GetInstance(typeof(ICommandHandler<T>));
            
            return cmdHandler;
        }

        private IEnumerable<Type> GetHandlerTypes<T>() where T : Command
        {
            List<Type> handlers = new List<Type>();
            var theHandlers = container.GetAllInstances(typeof(ICommandHandler<T>));

            foreach (var handler in theHandlers)
            {
                handlers.Add(handler.GetType());
            }

            //var handlers = typeof(ICommandHandler<>).Assembly.GetExportedTypes()
            //    .Where(x => x.GetInterfaces()
            //        .Any(a => a.IsGenericType && a.GetGenericTypeDefinition() == typeof(ICommandHandler<>)))
            //        .Where(h => h.GetInterfaces()
            //            .Any(ii => ii.GetGenericArguments()
            //                .Any(aa => aa == typeof(T)))).ToList();


            return handlers;
        }

    }
}
