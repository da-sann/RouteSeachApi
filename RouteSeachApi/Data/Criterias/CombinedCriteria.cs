using System;
using System.Linq.Expressions;
using RouteSeachApi.Helpers;
using RouteSeachApi.Interfaces.Data.Criterias;

namespace RouteSeachApi.Data.Criterias {
    public class CombinedCriteria<TEntity> : ICriteria<TEntity> where TEntity : class {
        public CombinedCriteria(ICriteria<TEntity> from, ICriteria<TEntity> to) {
            _from = from;
            _to = to;
        }
        private readonly ICriteria<TEntity> _from;
        private readonly ICriteria<TEntity> _to;

        public Expression<Func<TEntity, bool>> BuildCriteria() {
            return _from.BuildCriteria().And(_to.BuildCriteria());
        }
    }
}
