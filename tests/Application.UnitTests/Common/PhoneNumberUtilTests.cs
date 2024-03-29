using Application.Common;
using Xunit;

namespace Application.UnitTests.Common;

public class PhoneNumberUtilTests
{
    [Theory]
    [InlineData("+989121234567")]
    public void PhoneNumberUtil_Should_DetectCorrectPhoneNumber(string phoneNumber)
    {
        Assert.True(PhoneNumberUtil.IsMobileNumber(phoneNumber));
    }

    [Theory]
    [InlineData("+982188776655")]
    public void PhoneNumberUtil_Should_DetectInCorrectPhoneNumber(string phoneNumber)
    {
        Assert.False(PhoneNumberUtil.IsMobileNumber(phoneNumber));
    }
}
