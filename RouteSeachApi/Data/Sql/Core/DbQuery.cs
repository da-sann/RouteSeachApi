using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RouteSeachApi.Interfaces.Data;

namespace RouteSeachApi.Data.Sql.Core {
    public class DbQuery<TEntity> : IDbSet<TEntity> where TEntity : class {
        public DbQuery(DbSet<TEntity> set) {
            _set = set;
            _query = set;
        }
        private readonly DbSet<TEntity> _set;

        public EntityEntry Attach(TEntity entity) {
            return _set.Attach(entity);
        }

        public void Add(TEntity entity) {
            _set.Add(entity);
        }

        public void Remove(TEntity entity) {
            _set.Remove(entity);
        }

        public async ValueTask AddAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken)) {
            await _set.AddAsync(entity, cancellationToken);
        }

        public TEntity Find(object id) {
            return _set.Find(id);
        }

        public ValueTask<TEntity> FindAsync(object id, CancellationToken cancellationToken) {
            return _set.FindAsync(new[] { id }, cancellationToken);
        }

        #region IQueryable
        private readonly IQueryable<TEntity> _query;

        public Type ElementType => _query.ElementType;

        public Expression Expression => _query.Expression;

        public IQueryProvider Provider => _query.Provider;

        public IEnumerator<TEntity> GetEnumerator() {
            return _query.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public IQueryable<TEntity> FromSqlString(string sql, params object[] parameters) {
            return _set.FromSqlRaw(sql, parameters);
        }
        #endregion
    }
}
