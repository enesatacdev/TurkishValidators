using TurkishValidators.Validators;
using TurkishValidators.Options;
using Xunit;

namespace TurkishValidators.Tests.Validators
{
    public class TelefonNoValidatorTests
    {
        [Theory]
        [InlineData("05321234567", true)] // Valid GSM
        [InlineData("02121234567", true)] // Valid Landline
        [InlineData("905321234567", true)] // Valid with 90
        [InlineData("+905321234567", true)] // Valid with +90 (impl handles digits only so + is ignored by caller usually or handled)
        // Note: Our implementation extracts digits. + is not a digit so it is ignored.
        [InlineData("1234567890", false)] // Invalid
        public void Validate_ShouldReturnExpectedResult(string phone, bool expectedIsValid)
        {
            var validator = new TelefonNoValidator();
            var result = validator.Validate(phone);
            Assert.Equal(expectedIsValid, result.IsValid);
        }

        [Fact]
        public void Validate_ShouldFail_WhenGsmNotAllowed()
        {
            var options = new TelefonNoValidationOptions { AllowGsm = false };
            var validator = new TelefonNoValidator(options);
            var result = validator.Validate("05321234567");
            Assert.False(result.IsValid);
        }
    }
}
