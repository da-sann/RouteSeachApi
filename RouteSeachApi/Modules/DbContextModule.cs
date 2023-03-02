using System;
using System.IO;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RouteSeachApi.Data.Sql.Core;
using RouteSeachApi.Data.Sql.Core.Configuration;

namespace RouteSeachApi.Modules {
    public abstract class DbContextModule<TContext> : Module where TContext : BaseDbContext {
        protected static IConfiguration GetConfiguration() {
            return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        }

        private static IContainer _container;
        public TContext CreateDbContext(string[] args) {
            var builder = new ContainerBuilder();
            Load(builder);
            builder.RegisterInstance(_configuration);
            builder.RegisterType<TContext>();
            _container = builder.Build();
            return _container.Resolve<TContext>();
        }

        public DbContextModule(IConfiguration configuration) {
            _configuration = configuration;
            _contextName = typeof(TContext).Name;
            _optionsBuilder = new DbContextOptionsBuilder<TContext>();
            _optionsBuilder.UseSqlServer(_configuration.GetConnectionString(_contextName), a => {
                a.CommandTimeout((int)TimeSpan.FromMinutes(5).TotalSeconds);
                a.EnableRetryOnFailure(3);
                a.MigrationsAssembly(ThisAssembly.FullName);
                a.MigrationsHistoryTable(String.Format("__{0}Migrations", _contextName));
            });
        }
        private readonly IConfiguration _configuration;
        protected IConfiguration Configuration {
            get {
                return _configuration;
            }
        }
        private readonly string _contextName;
        private readonly DbContextOptionsBuilder<TContext> _optionsBuilder;

        protected override void Load(ContainerBuilder builder) {
            DbConfigurator.Register(ThisAssembly);
            builder.RegisterInstance(_optionsBuilder.Options);
            RegisterContext(builder);
            base.Load(builder);
        }

        protected virtual void RegisterContext(ContainerBuilder builder, params Type[] constructorArgs) {
            var registrator = builder.RegisterType<TContext>().AsImplementedInterfaces().InstancePerLifetimeScope();
            if (constructorArgs.Length > 0)
                registrator.UsingConstructor(constructorArgs);
        }
        protected override System.Reflection.Assembly ThisAssembly => GetType().Assembly;
    }
}
