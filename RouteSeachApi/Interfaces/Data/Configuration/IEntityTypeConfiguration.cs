using System;
using Microsoft.EntityFrameworkCore;

namespace RouteSeachApi.Interfaces.Data.Configuration {
    public interface IEntityTypeConfiguration {
        void Configure(ModelBuilder modelBuilder);
    }
}
