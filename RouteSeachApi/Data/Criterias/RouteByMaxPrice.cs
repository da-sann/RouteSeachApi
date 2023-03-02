using System;
using System.Linq.Expressions;
using RouteSeachApi.Data.Entities;

namespace RouteSeachApi.Data.Criterias {
    public class RouteByMaxPrice : ACriteria<Route> {
        public RouteByMaxPrice(decimal price) {
            _price = price;
        }

        private readonly decimal _price;

        public override Expression<Func<Route, bool>> BuildCriteria() {
            return u => u.Price <= _price;
        }
    }
}
