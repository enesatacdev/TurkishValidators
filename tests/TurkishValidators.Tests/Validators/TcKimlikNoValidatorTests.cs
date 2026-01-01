using TurkishValidators.Validators;
using TurkishValidators.Options;
using Xunit;

namespace TurkishValidators.Tests.Validators
{
    public class TcKimlikNoValidatorTests
    {
        [Theory]
        [InlineData("10000000146", true)] // Valid
        [InlineData("12345678901", false)] // Invalid
        [InlineData("11111111110", true, true)] // Test number (if allowed)
        [InlineData("11111111110", false)] // Test number (default not allowed)
        [InlineData("02345678901", false)] // Starts with 0
        [InlineData("123", false)] // Length
        [InlineData("abcdefghijk", false)] // Numeric
        public void Validate_ShouldReturnExpectedResult(string tcNo, bool expectedIsValid, bool allowTest = false)
        {
            var options = new TcKimlikNoValidationOptions { AllowTestNumbers = allowTest };
            var validator = new TcKimlikNoValidator(options);
            var result = validator.Validate(tcNo);

            Assert.Equal(expectedIsValid, result.IsValid);
        }
    }
}
