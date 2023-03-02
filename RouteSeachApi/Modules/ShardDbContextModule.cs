using System;
using Autofac;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using RouteSeachApi.Data.Sql.Core;
using RouteSeachApi.Interfaces.Data;
using RouteSeachApi.Services;

namespace RouteSeachApi.Modules {
    public class ShardDbContextModule : DbContextModule<ShardDbContext>, IDesignTimeDbContextFactory<ShardDbContext> {
        public ShardDbContextModule() : this(GetConfiguration()) { }
        public ShardDbContextModule(IConfiguration configuration) : base(configuration) { }

        protected sealed override void Load(ContainerBuilder builder) {
            base.Load(builder);
            builder.RegisterType<ShardMapManager<ShardDbContext>>();
            builder.RegisterType<ShardDbContextFactory>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<RequestShardNameProvider>().As<IShardNameProvider>();
            builder.RegisterType<SingleTenantProvider>().As<IShardNameProvider>();
        }

        protected sealed override void RegisterContext(ContainerBuilder builder, params Type[] constructorArgs) {
            builder.RegisterType<ShardDbContext>().UsingConstructor(typeof(ShardMapManager<ShardDbContext>), typeof(string)).AsSelf();
            builder.Register(c => c.Resolve<IShardDbContextFactory>().Create()).AsImplementedInterfaces();
        }
    }
}
