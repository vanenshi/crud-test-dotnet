using PhoneNumbers;
using GooglePhoneNumberUtil = PhoneNumbers.PhoneNumberUtil;

namespace Application.Common;

public static class MobileNumberValidator
{
    
    private static readonly PhoneNumberUtil PhoneUtil = GooglePhoneNumberUtil.GetInstance();

    public static bool IsPhoneNumber(string value)
    {
        if (!TryParseNumber(value, out PhoneNumber? phoneNumber))
            return false;

        return IsMobile(phoneNumber);
    }
    
    private static bool TryParseNumber(string value, out PhoneNumber? phoneNumber)
    {
        try
        {
            phoneNumber = PhoneUtil.Parse(value, null);
            return true;
        }
        catch (NumberParseException)
        {
            phoneNumber = null;
            return false;
        }
    }

    private static bool IsMobile(PhoneNumber? phoneNumber)
    {
        var type = PhoneUtil.GetNumberType(phoneNumber);
        return type == PhoneNumberType.MOBILE || type == PhoneNumberType.FIXED_LINE_OR_MOBILE;
    }
}