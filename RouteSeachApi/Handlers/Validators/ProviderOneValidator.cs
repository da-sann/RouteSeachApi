using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using RouteSeachApi.Interfaces.Services;
using RouteSeachApi.Models.Requests;

namespace RouteSeachApi.Handlers.Validators {
    public class ProviderOneValidator : AbstractValidator<ProviderOneSearchRequest> {
        public ProviderOneValidator(IProviderOneSearchService providerOneSearchService) {
            _providerOneSearchService = providerOneSearchService;

            RuleFor(x => x).MustAsync(IsAvailable).WithErrorCode("500").WithMessage("Service Unavailable")
                .When(m => m.OnlyCached != true);
        }

        IProviderOneSearchService _providerOneSearchService;

        private Task<bool> IsAvailable(ProviderOneSearchRequest request, CancellationToken token) {
            return _providerOneSearchService.IsAvailableAsync(token);
        }
    }
}
