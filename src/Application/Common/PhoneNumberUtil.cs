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
}
