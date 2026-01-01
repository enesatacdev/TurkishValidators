using System.Linq;
using System.Text.RegularExpressions;
using TurkishValidators.Options;
using TurkishValidators.Results;
using TurkishValidators.Config;

namespace TurkishValidators.Validators
{
    /// <summary>
    /// Türk telefon numarası doğrulama işlemlerini gerçekleştirir.
    /// </summary>
    public class TelefonNoValidator
    {
        private readonly TelefonNoValidationOptions _options;

        public TelefonNoValidator(TelefonNoValidationOptions? options = null)
        {
            _options = options ?? new TelefonNoValidationOptions();
        }

        public static bool IsValid(string? telefonNo)
        {
            return new TelefonNoValidator().Validate(telefonNo).IsValid;
        }

        public ValidationResult Validate(string? telefonNo)
        {
            var messages = TurkishValidatorConfig.GetMessages(_options.Culture);

            if (string.IsNullOrWhiteSpace(telefonNo))
            {
                return ValidationResult.Failure(messages.PhoneEmpty, "PhoneEmpty");
            }

            // Sadece rakamları al
            string digits = new string(telefonNo.Where(char.IsDigit).ToArray());

            // Uzunluk kontrolü ve format
            if (digits.StartsWith("90") && digits.Length == 12)
            {
                digits = digits.Substring(2);
            }
            else if (digits.StartsWith("0") && digits.Length == 11)
            {
                digits = digits.Substring(1);
            }
            
            if (digits.Length != 10)
            {
                return ValidationResult.Failure(messages.PhoneLength, "PhoneLength");
            }

            char firstChar = digits[0];
            bool isGsm = firstChar == '5';
            bool isLandline = firstChar == '2' || firstChar == '3' || firstChar == '4';
            bool isSpecial = firstChar == '8'; 

            if (isGsm && !_options.AllowGsm)
            {
                return ValidationResult.Failure(messages.PhoneGsmNotAllowed, "PhoneGsmNotAllowed");
            }

            if ((isLandline || isSpecial) && !_options.AllowLandlines)
            {
                return ValidationResult.Failure(messages.PhoneLandlineNotAllowed, "PhoneLandlineNotAllowed");
            }

            if (!isGsm && !isLandline && !isSpecial)
            {
                 return ValidationResult.Failure(messages.PhoneInvalidAreaCode, "PhoneInvalidAreaCode");
            }
            
            return ValidationResult.Success();
        }
    }
}
