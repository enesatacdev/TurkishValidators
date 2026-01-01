using System.Linq;
using System.Numerics;
using TurkishValidators.Options;
using TurkishValidators.Results;
using TurkishValidators.Config;

namespace TurkishValidators.Validators
{
    /// <summary>
    /// IBAN doğrulama işlemlerini gerçekleştirir.
    /// </summary>
    public class IbanValidator
    {
        private readonly IbanValidationOptions _options;

        public IbanValidator(IbanValidationOptions? options = null)
        {
            _options = options ?? new IbanValidationOptions();
        }

        public static bool IsValid(string? iban)
        {
            return new IbanValidator().Validate(iban).IsValid;
        }

        public ValidationResult Validate(string? iban)
        {
            var messages = TurkishValidatorConfig.GetMessages(_options.Culture);

            if (string.IsNullOrWhiteSpace(iban))
            {
                return ValidationResult.Failure(messages.IbanEmpty, "IbanEmpty");
            }

            // Boşlukları ve tireleri temizle
            string cleanIban = iban.Replace(" ", "").Replace("-", "").ToUpperInvariant();

            // Uzunluk kontrolü (TR IBAN 26 karakter)
            if (cleanIban.Length != 26)
            {
                return ValidationResult.Failure(messages.IbanLength, "IbanLength");
            }

            // Başlangıç kontrolü
            if (!cleanIban.StartsWith("TR"))
            {
                return ValidationResult.Failure(messages.IbanCountryCode, "IbanCountryCode");
            }

            if (!ValidateChecksum(cleanIban))
            {
                return ValidationResult.Failure(messages.IbanChecksum, "IbanChecksum");
            }

            return ValidationResult.Success();
        }

        private bool ValidateChecksum(string iban)
        {
            // İlk 4 karakteri sona al
            string rearranged = iban.Substring(4) + iban.Substring(0, 4);

            string numericIban = "";
            foreach (char c in rearranged)
            {
                if (char.IsLetter(c))
                {
                    numericIban += (c - 55).ToString();
                }
                else
                {
                    numericIban += c.ToString();
                }
            }

            if (BigInteger.TryParse(numericIban, out BigInteger number))
            {
                return (number % 97) == 1;
            }
            return false;
        }
    }
}
