using System;
using System.Linq.Expressions;
using RouteSeachApi.Data.Entities;

namespace RouteSeachApi.Data.Criterias {
    public class RouteByOrigin : ACriteria<Route> {
        public RouteByOrigin(string origin) {
            _origin = origin.Trim().ToLower();
        }

        private readonly string _origin;

        public override Expression<Func<Route, bool>> BuildCriteria() {
            return u => u.Origin.ToLower().Contains(_origin);
        }
    }
}
