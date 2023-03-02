using System;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using RouteSeachApi.Data.Entities;
using RouteSeachApi.Data.Sql.Core.Configuration;
using RouteSeachApi.Interfaces.Data;

namespace RouteSeachApi.Data.Sql.Core {
    [DebuggerDisplay("{ContextName}:{ShardName}:{Id}")]
    public class ShardDbContext : BaseDbContext, IShardDbContext {
        public ShardDbContext(ShardMapManager<ShardDbContext> shardMapManager, string shardName) : base(shardMapManager.GetOptions(shardName)) {
            ShardName = shardName;
        }
        public string ShardName { get; private set; }

        public ShardDbContext(ShardMapManager<ShardDbContext> shardMapManager) : base(shardMapManager.GetOptions()) {
            ShardName = string.Empty;
        }

        protected ShardDbContext(DbContextOptions<ShardDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            DbConfigurator.Configure<ShardDbContext>(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        #region DbSets
        public DbSet<Route> Routes { get; set; }

        #endregion
    }
}
