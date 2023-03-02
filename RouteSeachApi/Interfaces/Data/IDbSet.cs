using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace RouteSeachApi.Interfaces.Data {
    public interface IDbSet<TEntity> : IQueryable<TEntity>, IEnumerable<TEntity>, IEnumerable, IQueryable
        where TEntity : class {
        EntityEntry Attach(TEntity entity);
        TEntity Find(object id);
        ValueTask<TEntity> FindAsync(object id, CancellationToken cancellationToken);
        void Add(TEntity entity);
        void Remove(TEntity entity);
        ValueTask AddAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));
        IQueryable<TEntity> FromSqlString(string sql, params object[] parameters);
    }
}
