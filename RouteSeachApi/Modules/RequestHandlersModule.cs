using System;
using System.Linq;
using System.Reflection;
using Autofac;
using MediatR;
using MediatR.Pipeline;
using RouteSeachApi.Handlers.Behaviors;

namespace RouteSeachApi.Modules {
    public class RequestHandlersModule : Autofac.Module {
        protected override void Load(ContainerBuilder builder) {
            base.Load(builder);
            var handlers = ThisAssembly.GetTypes()
                .Where(t => t.IsClosedTypeOf(typeof(IRequestHandler<,>)) || t.IsClosedTypeOf(typeof(INotificationHandler<>)))
                .ToArray();
            Array.ForEach(handlers, e => builder.RegisterType(e).AsSelf().AsImplementedInterfaces());

            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

            builder.RegisterGeneric(typeof(RequestValidationBehavior<>)).As(typeof(IRequestPreProcessor<>));
            builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        }
    }
}
