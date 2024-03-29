using Application.Common;
using Xunit;

namespace Application.UnitTests.Common;

public class PhoneNumberUtilTests
{
    [Theory]
    [InlineData("+989000000")]
    [InlineData("+1-212-456-7890")]
    public void PhoneNumberUtil_Should_DetectCorrectPhoneNumber(string phoneNumber)
    {
        Assert.True(MobileNumberValidator.IsPhoneNumber(phoneNumber));
    }

    [Theory]
    [InlineData("000000000")]
    [InlineData("")]
    [InlineData("09033333333")]
    [InlineData("amir")]
    public void PhoneNumberUtil_Should_DetectInCorrectPhoneNumber(string phoneNumber)
    {
        Assert.False(MobileNumberValidator.IsPhoneNumber(phoneNumber));
    }
}
