using TurkishValidators.Validators;
using Xunit;

namespace TurkishValidators.Tests.Validators
{
    public class PlakaValidatorTests
    {
        [Theory]
        [InlineData("34ABC123", true)] // Valid
        [InlineData("34 ABC 123", true)] // Valid with spaces
        [InlineData("06 YZ 999", true)] // Valid Ankara
        [InlineData("99 ABC 123", false)] // Invalid City Code
        [InlineData("34 A 123456", false)] // Too long
        public void Validate_ShouldReturnExpectedResult(string plate, bool expectedIsValid)
        {
            var validator = new PlakaValidator();
            var result = validator.Validate(plate);
            Assert.Equal(expectedIsValid, result.IsValid);
        }
    }
}
