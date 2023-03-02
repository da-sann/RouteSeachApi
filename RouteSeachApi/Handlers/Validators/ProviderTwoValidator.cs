using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using RouteSeachApi.Interfaces.Services;
using RouteSeachApi.Models.Requests;

namespace RouteSeachApi.Handlers.Validators {
    public class ProviderTwoValidator : AbstractValidator<ProviderTwoSearchRequest> {
        public ProviderTwoValidator(IProviderTwoSearchService providerTwoSearchService) {
            _providerTwoSearchService = providerTwoSearchService;

            RuleFor(x => x).MustAsync(IsAvailable).WithErrorCode("500").WithMessage("Service Unavailable")
                .When(m => m.OnlyCached != true);
        }

        IProviderTwoSearchService _providerTwoSearchService;

        private Task<bool> IsAvailable(ProviderTwoSearchRequest request, CancellationToken token) {
            return _providerTwoSearchService.IsAvailableAsync(token);
        }
    }
}
