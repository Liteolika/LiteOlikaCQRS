using Core.ApplicationServices.CommandHandlers;
using Core.ApplicationServices.Eventhandlers;
using Core.ApplicationServices.Messaging;
using Core.Infrastructure.Database.EntityFramework;
using Core.Shared.CommandHandlers;
using Core.Shared.EventHandlers;
using Core.Shared.Messaging;
using Core.Shared.Storage;
using Core.Shared.ViewModels;
using Domain.Surveys.ViewModels;
using StructureMap;
using StructureMap.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ApplicationServices
{
    public sealed class ServiceLocator
    {

        private static ICommandBus _commandBus;
        private static ISurveyViewModelStore _surveyViewModelStore;
        private static bool _isInitialized;
        private static readonly object _lockThis = new object();
        private static IContainer _container;

        static ServiceLocator()
        {
            if (!_isInitialized)
            {
                lock (_lockThis)
                {
                    _container = ContainerBootstrapper.BootstrapStructureMap();
                    _commandBus = _container.GetInstance<ICommandBus>();
                    _surveyViewModelStore = _container.GetInstance<ISurveyViewModelStore>();
                    _isInitialized = true;
                }
            }
        }

        public static ICommandBus CommandBus
        {
            get { return _commandBus; }
        }

        public static ISurveyViewModelStore SurveyViewModels
        {
            get { return _surveyViewModelStore; }
        }

    }

    public static class ContainerBootstrapper
    {
        public static IContainer BootstrapStructureMap()
        {
            IContainer container = new Container();
            ConfigureContainer(container);
            return container;
        }

        public static IContainer BootstrapStructureMap(IContainer container)
        {
            ConfigureContainer(container);
            return container;
        }

        private static void ConfigureContainer(IContainer container)
        {

            string connectionStringName = "Default";

            container.Configure(cfg =>
            {
                cfg.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.AssembliesFromApplicationBaseDirectory();
                    scan.ConnectImplementationsToTypesClosing(typeof(ICommandHandler<>));
                    scan.ConnectImplementationsToTypesClosing(typeof(IEventHandler<>));
                    scan.WithDefaultConventions();
                });

                cfg.For(typeof(IEventRepository<>)).Use(typeof(EventRepository<>));
                cfg.For<IEventStorage>().Use<EventStorage>().Ctor<string>("connectionStringOrName").Is(connectionStringName);
                cfg.For<IEventBus>().Use<EventBus>();
                cfg.For<ICommandHandlerFactory>().Use<StructureMapCommandHandlerFactory>();
                cfg.For<IEventHandlerFactory>().Use<StructureMapEventHandlerFactory>();
                cfg.For<ICommandBus>().Use<CommandBus>();
                cfg.For<IEventBus>().Use<EventBus>();
                cfg.For<ISurveyViewModelStore>().Use<SurveyViewModelStore>().Ctor<string>("connectionStringOrName").Is(connectionStringName);
            });
        }




    }
}
