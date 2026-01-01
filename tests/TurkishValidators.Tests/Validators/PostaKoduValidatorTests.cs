using TurkishValidators.Validators;
using TurkishValidators.Options;
using Xunit;

namespace TurkishValidators.Tests.Validators
{
    public class PostaKoduValidatorTests
    {
        [Theory]
        [InlineData("34410", true)] // Valid
        [InlineData("99999", false)] // Invalid City Code
        [InlineData("123", false)] // Length
        public void Validate_ShouldReturnExpectedResult(string code, bool expectedIsValid)
        {
            var validator = new PostaKoduValidator();
            var result = validator.Validate(code);
            Assert.Equal(expectedIsValid, result.IsValid);
        }

        [Fact]
        public void Validate_ShouldCheckExpectedCity()
        {
            var options = new PostaKoduValidationOptions 
            { 
                ValidateCity = true,
                ExpectedCity = "İstanbul"
            };
            var validator = new PostaKoduValidator(options);

            // 34... -> İstanbul matches
            Assert.True(validator.Validate("34410").IsValid);

            // 06... -> Ankara does not match İstanbul
            Assert.False(validator.Validate("06100").IsValid);
        }
    }
}
