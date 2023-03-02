using System;
using System.Linq;
using AutoMapper;
using RouteSeachApi.Data.Criterias;
using RouteSeachApi.Models.Requests;
using RouteSeachApi.Models.Responses;
using RouteSeachApi.Models.ViewModels;

namespace RouteSeachApi.Mapper.Profiles {
    public class ModelToModelProfile : Profile {

        public ModelToModelProfile() {
            CreateMap<ProviderOneSearchRequest, SearchRequest>()
                .ForMember(m => m.Origin, e => e.MapFrom(m => m.From))
                .ForMember(m => m.Destination, e => e.MapFrom(m => m.To))
                .ForMember(m => m.OriginDateTime, e => e.MapFrom(m => m.DateFrom))
                .ForMember(m => m.Filters, e => e.MapFrom(m => new SearchFilters() {
                    DestinationDateTime = m.DateTo,
                    MaxPrice = m.MaxPrice,
                    OnlyCached = m.OnlyCached
                }));

            CreateMap<ProviderTwoSearchRequest, SearchRequest>()
                .ForMember(m => m.Origin, e => e.MapFrom(m => m.Departure))
                .ForMember(m => m.Destination, e => e.MapFrom(m => m.Arrival))
                .ForMember(m => m.OriginDateTime, e => e.MapFrom(m => m.DepartureDate))
                .ForMember(m => m.Filters, e => e.MapFrom(m => new SearchFilters() { MinTimeLimit = m.MinTimeLimit, OnlyCached = m.OnlyCached }));

            CreateMap<SearchResponse, ProviderOneSearchResponse>()
                .ForMember(m => m.Routes, e => e.MapFrom(m => m.Routes.Select(r => new ProviderOneRoute {
                    DateFrom = r.OriginDateTime,
                    DateTo = r.DestinationDateTime,
                    From = r.Origin,
                    To = r.Destination,
                    Price = r.Price,
                    TimeLimit = r.TimeLimit
                }).ToArray()));

            CreateMap<SearchResponse, ProviderTwoSearchResponse>()
                .ForMember(m => m.Routes, e => e.MapFrom(m => m.Routes.Select(r => new ProviderTwoRoute {
                    Departure = new ProviderTwoPoint { Date = r.OriginDateTime, Point = r.Origin },
                    Arrival = new ProviderTwoPoint { Date = r.DestinationDateTime, Point = r.Destination },
                    Price = r.Price,
                    TimeLimit = r.TimeLimit
                }).ToArray()));

            CreateMap<SearchRequest, RouteSearch>()
                .ForMember(m => m.OnlyCached, e => e.MapFrom(m => m.Filters.OnlyCached))
                .ForMember(m => m.MinTimeLimit, e => e.MapFrom(m => m.Filters.MinTimeLimit))
                .ForMember(m => m.MaxPrice, e => e.MapFrom(m => m.Filters.MaxPrice))
                .ForMember(m => m.DestinationDateTime, e => e.MapFrom(m => m.Filters.DestinationDateTime));
        }
    }
}
