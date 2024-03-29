using PhoneNumbers;
using GooglePhoneNumberUtil = PhoneNumbers.PhoneNumberUtil;

namespace Application.Common;

public static class PhoneNumberUtil
{
    public static bool IsPhoneNumber(string value)
    {
        var phoneUtil = GooglePhoneNumberUtil.GetInstance();
        try
        {
            var phoneNumber = phoneUtil.Parse(value, null);
            return phoneUtil.IsPossibleNumber(phoneNumber);
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static bool IsMobileNumber(string value)
    {
        var phoneUtil = GooglePhoneNumberUtil.GetInstance();
        try
        {
            var phoneNumber = phoneUtil.Parse(value, null);
            var numberType = phoneUtil.GetNumberType(phoneNumber);
            return numberType == PhoneNumberType.MOBILE;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
