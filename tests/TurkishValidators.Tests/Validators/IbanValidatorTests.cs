using TurkishValidators.Validators;
using Xunit;

namespace TurkishValidators.Tests.Validators
{
    public class IbanValidatorTests
    {
        [Theory]
        [InlineData("TR330006100519786457841326", true)] // Valid
        [InlineData("DE89370400440532013000", false)] // Foreign
        [InlineData("TR330006100519786457841327", false)] // Invalid Checksum
        [InlineData("TR33 0006 1005 1978 6457 8413 26", true)] // Spaces
        public void Validate_ShouldReturnExpectedResult(string iban, bool expectedIsValid)
        {
            var validator = new IbanValidator();
            var result = validator.Validate(iban);
            Assert.Equal(expectedIsValid, result.IsValid);
        }
    }
}
