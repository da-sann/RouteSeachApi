using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using FluentValidation;
using FluentValidation.Results;
using MediatR.Pipeline;
using RouteSeachApi.Handlers.Validators;

namespace RouteSeachApi.Handlers.Behaviors {
    public class RequestValidationBehavior<TRequest> : IRequestPreProcessor<TRequest> {
        public RequestValidationBehavior(IComponentContext container) {
            _container = container;
        }
        readonly IComponentContext _container;

        public async Task Process(TRequest request, CancellationToken cancellationToken) {
            var errors = new List<ValidationFailure>();
            foreach (var validator in _container.Resolve(typeof(IEnumerable<IValidator<TRequest>>)) as IEnumerable<IValidator<TRequest>>) {
                var result = await validator.ValidateAsync(request, cancellationToken);
                if (!result.IsValid)
                    errors.AddRange(result.Errors);
            }

            if (errors.Count > 0)
                throw new RequestValidationException(errors);
        }
    }
}
