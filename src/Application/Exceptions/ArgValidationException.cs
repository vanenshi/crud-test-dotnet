using System.Net;
using FluentValidation.Results;

namespace Application.Exceptions;

public class ArgValidationException : ApiException
{
    public ArgValidationException(IEnumerable<ValidationFailure> errors)
        : base(statusCode: HttpStatusCode.BadRequest, message: BuildErrorMessage(errors)) { }

    private static string BuildErrorMessage(IEnumerable<ValidationFailure> errors)
    {
        var arr = errors.Select(
            x => $"{Environment.NewLine} -- {x.PropertyName}: {x.ErrorMessage}"
        );
        return "Validation failed: " + string.Join(string.Empty, arr);
    }
}
