using System;

namespace RouteSeachApi.Interfaces.Data {
    public interface IDbContextFactory<T> where T : IDbContext {
        string GetConnectionString(string shard);
    }
}
