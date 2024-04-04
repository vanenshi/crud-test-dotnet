using Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PhoneNumbers;

namespace Infrastructure.Persistence.Converters;

public class PhoneNumberConverter(ConverterMappingHints mappingHints = null)
    : ValueConverter<string, ulong>(
        toDb => PhoneNumberStringToUlong(toDb),
        fromDb => UlongToPhoneNumberString(fromDb),
        mappingHints
    )
{
    private static ulong PhoneNumberStringToUlong(string phoneNumberString)
    {
        var phoneUtil = PhoneNumberUtil.GetInstance();
        var phoneNumber = phoneUtil.Parse(phoneNumberString, null);
        var phoneNumberE164 = phoneUtil.Format(phoneNumber, PhoneNumberFormat.E164);

        // Remove the '+' from the beginning of the number
        var phoneNumberWithoutPlus = phoneNumberE164.Substring(1);
        return ulong.Parse(phoneNumberWithoutPlus);
    }

    private static string UlongToPhoneNumberString(ulong phoneNumberUlong)
    {
        var phoneNumberString = "+" + phoneNumberUlong.ToString();
        return phoneNumberString;
    }
}
