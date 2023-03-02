using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RouteSeachApi.Interfaces.Data;

namespace RouteSeachApi.Data.Sql.Core {
    [DebuggerDisplay("{ContextName}:{Id}")]
    public abstract class BaseDbContext : DbContext, IDbContext, IDatabaseFacadeProvider {
        protected BaseDbContext(DbContextOptions options) : base(options) {
            if (this.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
                ConnectionString = this.Database.GetConnectionString();
            Id = Guid.NewGuid().ToString();
        }
        private static readonly Object obj = new();

        IDbSet<TEntity> IDbContext.Set<TEntity>() {
            lock (obj) {
                return new DbQuery<TEntity>(Set<TEntity>());
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            lock (obj) {
                base.OnConfiguring(optionsBuilder);
            }
        }

        public string Id { get; private set; }
        private bool _isDisposed;

        public string ConnectionString { get; }

        public string ContextName => GetType().Name;

        public override void Dispose() {
            if (_isDisposed) return;
            base.Dispose();
            _isDisposed = true;
            GC.SuppressFinalize(this);
        }

        private void DetachAll() {
            var entires = ChangeTracker.Entries().ToArray();
            foreach (var entry in entires) {
                if (entry.Entity != null)
                    entry.State = EntityState.Detached;
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
            var result = await base.SaveChangesAsync(cancellationToken);
            DetachAll();
            return result;
        }

        public override int SaveChanges() {
            var result = base.SaveChanges();
            DetachAll();
            return result;
        }

        public int SaveChangesWithoutDetach() {
            return base.SaveChanges();
        }

        public Task<int> SaveChangesWithoutDetachAsync(CancellationToken cancellationToken = default) {
            return base.SaveChangesAsync(cancellationToken);
        }

        public Task MigrateAsync(CancellationToken cancellationToken) {
            return Database.MigrateAsync(cancellationToken);
        }

        public async Task<string[]> GetMigrationsAsync(CancellationToken cancellationToken) {
            var migrations = await Database.GetPendingMigrationsAsync(cancellationToken);
            return migrations.ToArray();
        }
    }
}
