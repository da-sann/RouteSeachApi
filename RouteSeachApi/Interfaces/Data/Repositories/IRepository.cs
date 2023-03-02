using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using RouteSeachApi.Interfaces.Data.Entities;

namespace RouteSeachApi.Interfaces.Data.Repositories {
    public interface IRepository<TEntity, TKey> : IDisposable where TEntity : class, IEntity<TKey> {
        IQueryable<TEntity> GetAll();
        #region Methods
        bool Any(TKey id);
        bool Any(Expression<Func<TEntity, bool>> criteria);
        int Count();
        int Count(Expression<Func<TEntity, bool>> criteria);
        TEntity GetById(TKey id);
        TEntity Find(TKey id);
        IQueryable<TEntity> Get(TKey id);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> criteria);
        void Add(TEntity entity);
        TEntity Update(TKey id, Action<TEntity> func, params Expression<Func<TEntity, dynamic>>[] propertyToUpdate);
        void Delete(TKey id);
        void Delete(TEntity entity);
        void Save();
        void RemoveRange(Expression<Func<TEntity, bool>> criteria);
        int UpdateRange(Expression<Func<TEntity, bool>> criteria, Expression<Func<TEntity, TEntity>> updateFactory);
        #endregion

        #region Async Methods
        Task<bool> AnyAsync(TKey id, CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken = default(CancellationToken));
        Task<int> CountAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<int> CountAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken = default(CancellationToken));
        Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken = default(CancellationToken));
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken));
        Task SaveAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task RemoveRangeAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken = default(CancellationToken));
        Task ClearAsync(CancellationToken cancellationToken = default(CancellationToken));
        #endregion
    }
}
