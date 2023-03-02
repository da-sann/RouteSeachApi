using RouteSeachApi.Interfaces.Data.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RouteSeachApi.Data.Entities {
    public class Route : IEntity<Guid> {
        // Mandatory
        // Identifier of the whole route
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        // Mandatory
        // Start point of route
        public string Origin { get; set; }

        // Mandatory
        // End point of route
        public string Destination { get; set; }

        // Mandatory
        // Start date of route
        public DateTime OriginDateTime { get; set; }

        // Mandatory
        // End date of route
        public DateTime DestinationDateTime { get; set; }

        // Mandatory
        // Price of route
        public decimal Price { get; set; }

        // Mandatory
        // Timelimit. After it expires, route became not actual
        public DateTime TimeLimit { get; set; }

        public int GetRouteMinutes() {
            return (int)DestinationDateTime.Subtract(OriginDateTime).TotalMinutes;
        }
    }
}
