using System.Linq;
using TurkishValidators.Options;
using TurkishValidators.Results;
using TurkishValidators.Config;

namespace TurkishValidators.Validators
{
    /// <summary>
    /// Vergi Numarası (VKN) doğrulama işlemlerini gerçekleştirir.
    /// </summary>
    public class VergiNoValidator
    {
        private readonly VergiNoValidationOptions _options;

        public VergiNoValidator(VergiNoValidationOptions? options = null)
        {
            _options = options ?? new VergiNoValidationOptions();
        }

        public static bool IsValid(string? vergiNo)
        {
            return new VergiNoValidator().Validate(vergiNo).IsValid;
        }

        public ValidationResult Validate(string? vergiNo)
        {
            var messages = TurkishValidatorConfig.GetMessages(_options.Culture);

            if (string.IsNullOrWhiteSpace(vergiNo))
            {
                return ValidationResult.Failure(messages.VergiNoEmpty, "VergiNoEmpty");
            }

            if (vergiNo.Length != 10)
            {
                return ValidationResult.Failure(messages.VergiNoLength, "VergiNoLength");
            }

            if (!long.TryParse(vergiNo, out _))
            {
                return ValidationResult.Failure(messages.VergiNoNumeric, "VergiNoNumeric");
            }
            
            if (!ValidateChecksum(vergiNo))
            {
                return ValidationResult.Failure(messages.VergiNoChecksum, "VergiNoChecksum");
            }

            return ValidationResult.Success();
        }

        private bool ValidateChecksum(string vkn)
        {
            int total = 0;
            for (int i = 0; i < 9; i++)
            {
                int digit = vkn[i] - '0';
                int n = digit + (9 - i);
                int n1 = n % 10;
                int n2 = (n1 * (int)System.Math.Pow(2, 9 - i)) % 9;
                
                if (n1 != 0 && n2 == 0) n2 = 9;
                
                total += n2;
            }

            int lastDigit = vkn[9] - '0';
            int checksum = (10 - (total % 10)) % 10;

            return checksum == lastDigit;
        }
    }
}
