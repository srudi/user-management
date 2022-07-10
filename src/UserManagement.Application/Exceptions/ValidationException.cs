using FluentValidation.Results;

namespace UserManagement.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public Dictionary<string, string[]> Errors { get; }

        public ValidationException(IEnumerable<ValidationFailure> failures) : base("Validation error occured.")
        {
            Errors = failures.GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                             .ToDictionary(g => g.Key, g => g.ToArray());
        }
    }
}
