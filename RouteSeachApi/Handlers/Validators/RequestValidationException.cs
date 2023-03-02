using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using FluentValidation.Results;

namespace RouteSeachApi.Handlers.Validators {
    public class RequestValidationException : ValidationException {
        private static string _message = "Validation Error: ";
        public RequestValidationException(string message) : base(message) { }

        public RequestValidationException(IEnumerable<ValidationFailure> errors) : base(CreateMessage(errors), errors) { }

        private static string CreateMessage(IEnumerable<ValidationFailure> errors) {
            var builder = new StringBuilder();
            builder.AppendLine(_message);
            foreach (var err in errors) {
                var name = string.IsNullOrEmpty(err.PropertyName) ? "--" : err.PropertyName;
                builder.AppendLine(string.Format("{0} : {1}", name, err.ErrorMessage));
            }
            return builder.ToString();
        }
    }
}
