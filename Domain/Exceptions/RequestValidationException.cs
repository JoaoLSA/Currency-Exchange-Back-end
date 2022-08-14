using FluentValidation.Results;
using System.Runtime.Serialization;

namespace CurrencyExchange.Infrastructure
{
    [Serializable]
    public class RequestValidationException : Exception
    {

        public RequestValidationException(List<ValidationFailure> failures) : base("Validation error")
        {
            Failures = failures;
        }

        protected RequestValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Failures = new List<ValidationFailure>();
        }

        public List<ValidationFailure> Failures { get; }
    }
}