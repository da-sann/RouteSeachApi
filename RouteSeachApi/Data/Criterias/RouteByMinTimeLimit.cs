using System;
using RouteSeachApi.Data.Entities;
using System.Linq.Expressions;

namespace RouteSeachApi.Data.Criterias {
    public class RouteByMinTimeLimit : ACriteria<Route> {
        public RouteByMinTimeLimit(DateTime date) {
            _date = date;
        }

        private readonly DateTime _date;

        public override Expression<Func<Route, bool>> BuildCriteria() {
            return u => u.TimeLimit >= _date;
        }
    }
}
