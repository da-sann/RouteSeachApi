using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RouteSeachApi.Interfaces.Data;

namespace RouteSeachApi.Data.Sql.Core {
    public class ShardMapManager<TContext> where TContext : BaseDbContext {
        public ShardMapManager(IConfiguration configuration, IShardNameProvider shardNameProvider) {
            _configuration = configuration;
            _contextType = typeof(TContext);
            _builder = new DbContextOptionsBuilder<TContext>();
            _shardNameProvider = shardNameProvider;
        }
        private readonly IShardNameProvider _shardNameProvider;
        private readonly DbContextOptionsBuilder<TContext> _builder;
        private readonly Type _contextType;
        private readonly IConfiguration _configuration;

        public DbContextOptions<TContext> GetOptions(string name = "") {
            string connectionStringName = _contextType.Name;

            if (name.Length == 0) {
                name = _shardNameProvider.GetKey();
            }

            if (name.Length > 0) {
                connectionStringName = $"shard{name}";
            }

            string connectionString = _configuration.GetConnectionString(connectionStringName);

            if (string.IsNullOrWhiteSpace(connectionString)) {
                throw new Exception($"Connection string '{connectionStringName}' was not found for DbContext '{_contextType.Name}'.");
            }

            _builder.UseSqlServer(connectionString, a => {
                a.CommandTimeout((int)TimeSpan.FromMinutes(5).TotalSeconds);
                a.EnableRetryOnFailure(3);
                a.MigrationsAssembly(_contextType.Assembly.FullName);
                a.MigrationsHistoryTable(String.Format("__{0}Migrations", _contextType.Name));
            });

            return _builder.Options;
        }
    }
}
