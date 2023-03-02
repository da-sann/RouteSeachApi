using Autofac;
using System;
using System.Linq;

namespace RouteSeachApi.Modules {
    public class RepositoryModule : Module {
        private const string RepositoryPostfix = "Repository";
        protected override void Load(ContainerBuilder builder) {
            base.Load(builder);
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(a => !a.IsAbstract && a.Name.EndsWith(RepositoryPostfix))
                .AsImplementedInterfaces();
        }
    }
}
