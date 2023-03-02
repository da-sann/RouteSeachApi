using System;
using System.Linq;
using Autofac;
using FluentValidation;

namespace RouteSeachApi.Modules {
    public class ValidationModule : Module {
        protected override void Load(ContainerBuilder builder) {
            base.Load(builder);
            var validators = ThisAssembly.GetTypes().Where(t => t.IsClosedTypeOf(typeof(IValidator<>))).ToArray();
            Array.ForEach(validators, v => builder.RegisterType(v).AsSelf().AsImplementedInterfaces());
        }
    }
}
