using TurkishValidators.Enums;
using TurkishValidators.Validators;
using Xunit;

namespace TurkishValidators.Tests.Validators
{
    public class CreditCardValidatorTests
    {
        [Theory]
        [InlineData("454360", true)] // Visa (İş)
        [InlineData("554960", true)] // MC (Garanti)
        [InlineData("979200", true)] // Troy
        public void IsValid_ShouldValidateLuhn_Dynamic(string bin, bool expected)
        {
            var validNumber = GenerateValidCardNumber(bin);
            var result = CreditCardValidator.IsValid(validNumber);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void IsValid_ShouldFail_InvalidChecksum()
        {
             // 4111 1111 1111 1112 -> Sum 31 (Invalid)
             Assert.False(CreditCardValidator.IsValid("4111111111111112")); 
        }

        [Fact]
        public void GetBinInfo_ShouldIdentify_Ziraat_Visa()
        {
            var number = GenerateValidCardNumber("469884");
            var result = CreditCardValidator.GetBinInfo(number);

            Assert.True(result.IsValid, $"Generated number {number} should be valid");
            Assert.Equal(CardType.Visa, result.CardType);
            Assert.Equal("Ziraat Bankası", result.BankName);
        }

        [Fact]
        public void GetBinInfo_ShouldIdentify_Garanti_Mastercard()
        {
            var number = GenerateValidCardNumber("554960"); 
            var result = CreditCardValidator.GetBinInfo(number);

            Assert.True(result.IsValid);
            Assert.Equal(CardType.Mastercard, result.CardType);
            Assert.Equal("Garanti BBVA", result.BankName);
        }
        
        [Fact]
        public void GetBinInfo_ShouldIdentify_Troy()
        {
            var number = GenerateValidCardNumber("979200");
            var result = CreditCardValidator.GetBinInfo(number);

            Assert.True(result.IsValid);
            Assert.Equal(CardType.Troy, result.CardType);
            Assert.Equal("Troy Kart", result.BankName);
        }

        [Fact]
        public void GetBinInfo_ShouldIdentify_UnknownBank()
        {
            var number = GenerateValidCardNumber("411111"); // Unknown bin
            var result = CreditCardValidator.GetBinInfo(number);

            Assert.True(result.IsValid);
            Assert.Equal(CardType.Visa, result.CardType);
            Assert.Equal("Bilinmeyen Banka", result.BankName);
        }

        private string GenerateValidCardNumber(string bin)
        {
            // Pad to 15 digits (leave last one for check digit)
            // Length 16 total.
            var partial = bin.PadRight(15, '0');
            
            int sum = 0;
            bool checkDigit = true; // Algorithm typically doubles every second from right.
                                    // For 16 digits, check digit is at index 15 (not doubled).
                                    // Index 14 (doubled).
                                    // We are calculating sum for first 15 digits relative to the 16th.
            
            // Standard Luhn:
            // "The check digit (x) is obtained by computing the sum of the non-check digits then computing 9 times that value modulo 10." -> No that's simpler method.
            // Let's reverse standard loop:
            // We want (Sum + x) % 10 == 0.
            // We iterate from rightmost of partial (index 14).
            // Index 14 (will be index 14 in full number) -> correspond to "checkDigit=true" in our original loop because rightmost (15) is false.
            
            for (int i = 14; i >= 0; i--)
            {
                int digit = partial[i] - '0';
                if (checkDigit)
                {
                    digit *= 2;
                    if (digit > 9) digit -= 9;
                }
                sum += digit;
                checkDigit = !checkDigit;
            }

            int mod = sum % 10;
            int check = (mod == 0) ? 0 : 10 - mod;
            
            return partial + check;
        }
    }
}
