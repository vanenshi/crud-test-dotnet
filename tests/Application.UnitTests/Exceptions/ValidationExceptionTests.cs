using System.Net;
using Application.Exceptions;
using FluentValidation.Results;
using Xunit;

namespace Application.UnitTests.Exceptions;

public class ValidationExceptionTests
{
    [Fact]
    public void ArgValidation_Should_CreatesErrorContainTheDetails()
    {
        var failures = new List<ValidationFailure>
        {
            new("Age", "must be 18 or older"),
            new("Password", "must contain at least 8 characters"),
        };
        var validationFailure = new ArgValidationException(failures);

        Assert.Contains("Age: must be 18 or older", validationFailure.Message);
        Assert.Contains("Password: must contain at least 8 characters", validationFailure.Message);
    }

    [Fact]
    public void ArgValidation_Should_CreatesCorrectApiErrorCode()
    {
        var validationFailure = new ArgValidationException(new List<ValidationFailure>());
        Assert.Equal(HttpStatusCode.BadRequest, validationFailure.StatusCode);
    }
}
