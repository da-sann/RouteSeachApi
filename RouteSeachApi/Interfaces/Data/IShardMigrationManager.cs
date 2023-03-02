using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RouteSeachApi.Interfaces.Data {
    public interface IShardMigrationManager {
        Task MigrateAsync(CancellationToken cancellationToken);
        Task<IDictionary<string, string[]>> GetPendingMigrationsAsync(CancellationToken cancellationToken);
        bool IsCorrectKey(string key);
    }
}
