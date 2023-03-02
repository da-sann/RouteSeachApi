using System;
using System.Linq.Expressions;
using RouteSeachApi.Interfaces.Data.Criterias;

namespace RouteSeachApi.Data.Criterias {
    public class TrueCriteria<TEntity> : ICriteria<TEntity> where TEntity : class {
        public Expression<Func<TEntity, bool>> BuildCriteria() {
            return a => true;
        }
    }
}
