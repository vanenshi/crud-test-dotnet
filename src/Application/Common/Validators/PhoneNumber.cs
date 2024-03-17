using FluentValidation;
using FluentValidation.Validators;
using PhoneNumbers;

namespace Application.Common.Validators;

public class PhoneNumber<T> : PropertyValidator<T, string>
{
    public override bool IsValid(ValidationContext<T> context, string value)
    {
        return PhoneNumberUtil.IsPhoneNumber(value);
    }

    public override string Name => "PhoneNumberValidator";

    protected override string GetDefaultMessageTemplate(string errorCode) =>
        "{PropertyName} is not a valid phone number";
}
