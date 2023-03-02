using System;
using RouteSeachApi.Models.ViewModels;

namespace RouteSeachApi.Models.Responses {
    public class ProviderTwoSearchResponse {
        // Mandatory
        // Array of routes
        public ProviderTwoRoute[] Routes { get; set; }
    }
}
