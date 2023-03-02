using System;
using RouteSeachApi.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RouteSeachApi.Data.Configurations {
    public class RouteConfiguraion : EntityTypeConfiguration<Route> {
        public override void Configure(EntityTypeBuilder<Route> builder) {

        }
    }
}
