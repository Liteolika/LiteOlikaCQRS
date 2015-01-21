// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IoC.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


using Core.ApplicationServices.CommandHandlers;
using Core.ApplicationServices.Eventhandlers;
using Core.ApplicationServices.Messaging;
using Core.Infrastructure.Database.EntityFramework;
using Core.Shared.CommandHandlers;
using Core.Shared.EventHandlers;
using Core.Shared.Messaging;
using Core.Shared.Storage;
using Domain.Surveys.ViewModels;
using StructureMap;
using StructureMap.Graph;
using StructureMap.Web;
using StructureMap.Web.Pipeline;

namespace Test.WebApplication.DependencyResolution
{
    public static class IoC
    {
        public static IContainer Initialize()
        {
            IContainer container = new Container();

            container.Configure(cfg =>
            {
                cfg.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    //scan.AddAllTypesOf(typeof(ICommandHandler<>));
                    //scan.AddAllTypesOf(typeof(IEventHandler<>));
                    scan.ConnectImplementationsToTypesClosing(typeof(ICommandHandler<>));
                    scan.ConnectImplementationsToTypesClosing(typeof(IEventHandler<>));
                    scan.WithDefaultConventions();
                });

                string connectionStringName = "Default";

                cfg.For(typeof(IEventRepository<>)).HybridHttpOrThreadLocalScoped().Use(typeof(EventRepository<>));
                cfg.For<IEventStorage>().HybridHttpOrThreadLocalScoped().Use<EventStorage>().Ctor<string>("connectionStringOrName").Is(connectionStringName);
                cfg.For<IEventBus>().HybridHttpOrThreadLocalScoped().Use<EventBus>();
                cfg.For<ICommandHandlerFactory>().HybridHttpOrThreadLocalScoped().Use<StructureMapCommandHandlerFactory>();
                cfg.For<IEventHandlerFactory>().HybridHttpOrThreadLocalScoped().Use<StructureMapEventHandlerFactory>();
                cfg.For<ICommandBus>().HybridHttpOrThreadLocalScoped().Use<CommandBus>();
                cfg.For<IEventBus>().HybridHttpOrThreadLocalScoped().Use<EventBus>();
                cfg.For<ISurveyViewModelStore>().HybridHttpOrThreadLocalScoped().Use<SurveyViewModelStore>().Ctor<string>("connectionStringOrName").Is(connectionStringName);

            });

            return container;
        }
    }
}