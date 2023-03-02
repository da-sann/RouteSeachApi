using System;
using System.Linq.Expressions;
using RouteSeachApi.Data.Entities;

namespace RouteSeachApi.Data.Criterias {
    public class RouteByDestinationDate : ACriteria<Route> {
        public RouteByDestinationDate(DateTime date) {
            _date = date;
        }

        private readonly DateTime _date;

        public override Expression<Func<Route, bool>> BuildCriteria() {
            return u => u.DestinationDateTime == _date;
        }
    }
}
