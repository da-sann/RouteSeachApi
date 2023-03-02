using System;
using System.Linq.Expressions;
using RouteSeachApi.Data.Entities;
using RouteSeachApi.Helpers;

namespace RouteSeachApi.Data.Criterias {
    public class RouteSearch : ACriteria<Route> {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime? OriginDateTime { get; set; }
        public DateTime? DestinationDateTime { get; set; }
        public decimal? MaxPrice { get; set; }
        public DateTime? MinTimeLimit { get; set; }
        public bool? OnlyCached { get; set; }

        public override Expression<Func<Route, bool>> BuildCriteria() {
            var criteria = True;
            if (!string.IsNullOrEmpty(Origin))
                criteria = criteria.And(new RouteByOrigin(Origin));
            if (!string.IsNullOrEmpty(Destination))
                criteria = criteria.And(new RouteByDestination(Destination));
            if (DestinationDateTime.HasValue)
                criteria = criteria.And(new RouteByDestinationDate(DestinationDateTime.Value));
            if (MaxPrice.HasValue)
                criteria = criteria.And(new RouteByMaxPrice(MaxPrice.Value));
            if (MinTimeLimit.HasValue)
                criteria = criteria.And(new RouteByMinTimeLimit(MinTimeLimit.Value));
            if (OriginDateTime.HasValue)
                criteria = criteria.And(new RouteByOriginDate(OriginDateTime.Value));

            return criteria.BuildCriteria();
        }
    }
}
