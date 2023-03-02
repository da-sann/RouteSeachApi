using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RouteSeachApi.Interfaces.Data {
    public interface IDbContext : IDisposable {
        Task MigrateAsync(CancellationToken cancellationToken);
        string ContextName { get; }
        Task<string[]> GetMigrationsAsync(CancellationToken cancellationToken);
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        IModel Model { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        int SaveChanges();
        string ConnectionString { get; }
        string Id { get; }
    }
}
