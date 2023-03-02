using System;
using RouteSeachApi.Data.Entities;

namespace RouteSeachApi.Interfaces.Data.Repositories {
    public interface IRouteRepository : IRepository<Route, Guid> {
    }
}
