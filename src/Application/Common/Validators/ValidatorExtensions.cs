using FluentValidation;

namespace Application.Common.Validators;

public static class ValidatorExtensions
{
    public static void PhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        ruleBuilder.SetValidator(new PhoneNumber<T>());
    }
}
