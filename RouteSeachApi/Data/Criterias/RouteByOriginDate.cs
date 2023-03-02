using System;
using System.Linq.Expressions;
using RouteSeachApi.Data.Entities;

namespace RouteSeachApi.Data.Criterias {
    public class RouteByOriginDate : ACriteria<Route> {
        public RouteByOriginDate(DateTime date) {
            _date = date;
        }

        private readonly DateTime _date;

        public override Expression<Func<Route, bool>> BuildCriteria() {
            return u => u.OriginDateTime == _date;
        }
    }
}
