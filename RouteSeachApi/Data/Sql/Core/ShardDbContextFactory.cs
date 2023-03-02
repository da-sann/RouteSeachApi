using System;
using System.Collections.Concurrent;
using System.Linq;
using Autofac;
using Microsoft.Extensions.Configuration;
using RouteSeachApi.Interfaces.Data;

namespace RouteSeachApi.Data.Sql.Core {
    public class ShardDbContextFactory : IShardDbContextFactory {
        public ShardDbContextFactory(IConfiguration config, IComponentContext container, IShardNameProvider nameProvider) {
            _config = config;
            _container = container;
            _shards = new ConcurrentDictionary<string, IShardDbContext>();
            _nameProvider = nameProvider;
        }
        private readonly IComponentContext _container;
        private readonly IConfiguration _config;
        private readonly ConcurrentDictionary<string, IShardDbContext> _shards;
        private readonly IShardNameProvider _nameProvider;

        private IShardDbContext GetContext(string shard) {
            return _shards.GetOrAdd(shard, _container.Resolve<ShardDbContext>(new TypedParameter(typeof(string), shard)));
        }

        public IShardDbContext Create(string shard = null) {
            if (string.IsNullOrWhiteSpace(shard))
                shard = _nameProvider.GetKey();
            return GetContext(shard);
        }

        public string GetConnectionString(string shard) {
            string connStrName = $"shard{shard}";
            return _config.GetConnectionString(connStrName);
        }

        public string[] GetShardNames() => _config.GetSection("ConnectionStrings").GetChildren().AsEnumerable().Where(a => a.Key.StartsWith("shard") && !a.Key.Contains("_")).Select(a => a.Key.Substring(5)).ToArray();

        public void Dispose() {
            foreach (var context in _shards.Values)
                context.Dispose();
        }
    }
}
