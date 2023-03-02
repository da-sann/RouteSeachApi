using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RouteSeachApi.Interfaces.Data;
using RouteSeachApi.Interfaces.Data.Entities;
using RouteSeachApi.Interfaces.Data.Repositories;

namespace RouteSeachApi.Data.Repositories {
    public abstract class BaseRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey> {
        protected BaseRepository(IDbContext context) {
            _context = context;
            _set = _context.Set<TEntity>();
        }
        private IDbContext _context;
        private readonly IDbSet<TEntity> _set;
        protected IDbSet<TEntity> Set {
            get {
                return _set;
            }
        }
        public virtual void Add(TEntity entity) {
            _set.Add(entity);
        }

        public bool Any(TKey id) {
            return Where(a => a.Id.Equals(id)).Any();
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken = default(CancellationToken)) {
            return Where(criteria).AnyAsync(cancellationToken);
        }

        public Task<bool> AnyAsync(TKey id, CancellationToken cancellationToken = default(CancellationToken)) {
            return AnyAsync(a => a.Id.Equals(id), cancellationToken);
        }

        public virtual bool Any(Expression<Func<TEntity, bool>> criteria) {
            return Where(criteria).Any();
        }

        public int Count() {
            return GetAll().Count();
        }

        public int Count(Expression<Func<TEntity, bool>> criteria) {
            return Where(criteria).Count();
        }

        public Task<int> CountAsync(CancellationToken cancellationToken = default(CancellationToken)) {
            return GetAll().CountAsync(cancellationToken);
        }

        public Task<int> CountAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken = default(CancellationToken)) {
            return Where(criteria).CountAsync(criteria, cancellationToken);
        }

        public virtual async Task AddOrUpdate(TEntity entity, CancellationToken cancellationToken = default(CancellationToken)) {
            if (!await AnyAsync(entity.Id, cancellationToken))
                await AddAsync(entity, cancellationToken);
            else
                Update(entity);
        }

        public virtual Task AddAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken)) {
            return _set.AddAsync(entity, cancellationToken).AsTask();
        }

        public virtual Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken)) {
            return Task.WhenAll(entities.Select(a => AddAsync(a, cancellationToken)).ToArray());
        }

        public virtual void Update(TEntity entity) { }

        public TEntity Update(TKey id, Action<TEntity> func, params Expression<Func<TEntity, dynamic>>[] propertyToUpdate) {
            var entity = CreateInstance(id);
            var entry = _set.Attach(entity);
            func(entity);
            if (propertyToUpdate.Length > 0)
                ForceModified(entry, propertyToUpdate);
            Update(entity);
            return entity;
        }

        protected virtual TEntity CreateInstance(TKey id) {
            var entity = Activator.CreateInstance<TEntity>();
            entity.Id = id;
            return entity;
        }

        private void ForceModified(EntityEntry entry, Expression<Func<TEntity, dynamic>>[] properties) {
            var list = properties.Select(a => GetPropertyByExpression(a.Body)).ToArray();
            Array.ForEach(list, p => entry.Property(p).IsModified = true);
        }

        private string GetPropertyByExpression(Expression expression) {
            if (expression is UnaryExpression)
                expression = (expression as UnaryExpression).Operand;
            if (expression is MemberExpression) {
                var member = expression as MemberExpression;
                return member.Member.Name;
            }
            throw new NotSupportedException("MemberExpressions are supported only.");
        }

        public void Delete(TKey id) {
            var entity = CreateInstance(id);
            _set.Attach(entity);
            Delete(entity);
        }

        public virtual void Delete(TEntity entity) {
            _set.Remove(entity);
        }

        public virtual IQueryable<TEntity> GetAll() {
            return _set.AsNoTracking();
        }

        public virtual TEntity GetById(TKey id) {
            return GetAll().SingleOrDefault(a => a.Id.Equals(id));
        }

        public TEntity Find(TKey id) {
            return _set.Find(id);
        }

        public async virtual Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken = default(CancellationToken)) {
            return await GetAll().SingleOrDefaultAsync(a => a.Id.Equals(id), cancellationToken);
        }

        public Task<TEntity> FindAsync(TKey id, CancellationToken cancellationToken) {
            return _set.FindAsync(id, cancellationToken).AsTask();
        }

        public virtual IQueryable<TEntity> Get(TKey id) {
            return GetAll().Where(a => a.Id.Equals(id));
        }

        public void Save() {
            _context.SaveChanges();
        }

        public Task SaveAsync(CancellationToken cancellationToken = default(CancellationToken)) {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> criteria) {
            return GetAll().Where(criteria);
        }

        public virtual Task RemoveRangeAsync(Expression<Func<TEntity, bool>> criteria, CancellationToken cancellationToken = default(CancellationToken)) {
            return GetAll().Where(criteria).DeleteAsync(cancellationToken);
        }

        public virtual Task ClearAsync(CancellationToken cancellationToken = default(CancellationToken)) {
            return GetAll().DeleteAsync(cancellationToken);
        }

        protected void Dispose(bool flag) {
            if (flag) {
                if (_context != null)
                    _context.Dispose();
            }
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void RemoveRange(Expression<Func<TEntity, bool>> criteria) {
            GetAll().Where(criteria).Delete();
        }

        public void Attach(TEntity entity) {
            _set.Attach(entity);
        }

        public int UpdateRange(Expression<Func<TEntity, bool>> criteria, Expression<Func<TEntity, TEntity>> updateFactory) {
            return Where(criteria).Update(updateFactory);
        }

        public Task<int> UpdateRangeAsync(Expression<Func<TEntity, bool>> criteria, Expression<Func<TEntity, TEntity>> updateFactory, CancellationToken token) {
            return Where(criteria).UpdateAsync(updateFactory, token);
        }

        ~BaseRepository() {
            Dispose(false);
        }
    }
}
