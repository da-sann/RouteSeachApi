using RouteSeachApi.Models.ViewModels;
using System;

namespace RouteSeachApi.Models.Responses {
    public class ProviderOneSearchResponse {
        // Mandatory
        // Array of routes
        public ProviderOneRoute[] Routes { get; set; }
    }
}
