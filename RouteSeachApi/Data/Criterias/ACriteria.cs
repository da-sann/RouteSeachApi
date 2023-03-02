using System;
using System.Linq.Expressions;
using RouteSeachApi.Interfaces.Data.Criterias;

namespace RouteSeachApi.Data.Criterias {
    public abstract class ACriteria<TEntity> : ICriteria<TEntity> where TEntity : class {
        public abstract Expression<Func<TEntity, bool>> BuildCriteria();
        protected ICriteria<TEntity> True {
            get { return new TrueCriteria<TEntity>(); }
        }
    }
}
