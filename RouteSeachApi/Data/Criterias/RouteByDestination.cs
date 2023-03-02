using System;
using System.Linq.Expressions;
using RouteSeachApi.Data.Entities;

namespace RouteSeachApi.Data.Criterias {
    public class RouteByDestination : ACriteria<Route> {
        public RouteByDestination(string destination) {
            _destination = destination.Trim().ToLower();
        }

        private readonly string _destination;

        public override Expression<Func<Route, bool>> BuildCriteria() {
            return u => u.Destination.ToLower().Contains(_destination);
        }
    }
}
