using System;
using System.Linq.Expressions;

namespace RouteSeachApi.Interfaces.Data.Criterias {
    public interface ICriteria<TEntity> where TEntity : class {
        Expression<Func<TEntity, bool>> BuildCriteria();
    }
}
