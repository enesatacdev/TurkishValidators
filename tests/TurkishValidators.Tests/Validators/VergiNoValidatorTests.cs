using TurkishValidators.Validators;
using Xunit;

namespace TurkishValidators.Tests.Validators
{
    public class VergiNoValidatorTests
    {
        [Theory]
        [InlineData("1234567890", true)] // Valid
        [InlineData("1111111112", false)] // Invalid
        [InlineData("123", false)] // Length
        public void Validate_ShouldReturnExpectedResult(string vkn, bool expectedIsValid)
        {
            var validator = new VergiNoValidator();
            var result = validator.Validate(vkn);
            Assert.Equal(expectedIsValid, result.IsValid);
        }
    }
}
