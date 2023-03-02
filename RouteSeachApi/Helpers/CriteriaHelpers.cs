using System;
using RouteSeachApi.Data.Criterias;
using RouteSeachApi.Interfaces.Data.Criterias;

namespace RouteSeachApi.Helpers {
    public static class CriteriaHelpers {
        public static ICriteria<TEntity> And<TEntity>(this ICriteria<TEntity> from, ICriteria<TEntity> to) where TEntity : class {
            if (to != null)
                return new CombinedCriteria<TEntity>(from, to);
            else
                return from;
        }
    }
}
