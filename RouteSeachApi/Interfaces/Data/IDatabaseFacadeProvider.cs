using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace RouteSeachApi.Interfaces.Data {
    public interface IDatabaseFacadeProvider {
        DatabaseFacade Database { get; }
    }
}
