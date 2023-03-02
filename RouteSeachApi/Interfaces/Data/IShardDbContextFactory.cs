using System;

namespace RouteSeachApi.Interfaces.Data {
    public interface IShardDbContextFactory : IDbContextFactory<IShardDbContext>, IDisposable {
        string[] GetShardNames();
        IShardDbContext Create(string shard = null);
    }
}
