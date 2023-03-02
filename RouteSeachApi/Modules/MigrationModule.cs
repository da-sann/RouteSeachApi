using System;
using System.Collections.Generic;
using Autofac;
using Microsoft.Extensions.Configuration;
using RouteSeachApi.Data.Sql.Core;
using RouteSeachApi.Interfaces.Data;

namespace RouteSeachApi.Modules {
    public class MigrationModule : Module {
        public MigrationModule(IConfiguration configuration) {
            _configuration = configuration;
        }

        private readonly IConfiguration _configuration;

        protected override void Load(ContainerBuilder builder) {
            base.Load(builder);
            var key = _configuration["MigrationKey"];
            builder.Register(container => new ShardMigrationManager(key, container.Resolve<IEnumerable<IShardDbContext>>())).As<IShardMigrationManager>();
        }
    }
}
