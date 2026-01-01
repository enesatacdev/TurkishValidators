using System;
using System.Linq;
using TurkishValidators.Options;
using TurkishValidators.Results;
using TurkishValidators.Config;

namespace TurkishValidators.Validators
{
    /// <summary>
    /// TC Kimlik Numarası doğrulama işlemlerini gerçekleştirir.
    /// </summary>
    public class TcKimlikNoValidator
    {
        private readonly TcKimlikNoValidationOptions _options;

        /// <summary>
        /// TcKimlikNoValidator sınıfının yeni bir örneğini başlatır.
        /// </summary>
        /// <param name="options">Doğrulama seçenekleri. Verilmezse varsayılan değerler kullanılır.</param>
        public TcKimlikNoValidator(TcKimlikNoValidationOptions? options = null)
        {
            _options = options ?? new TcKimlikNoValidationOptions();
        }

        /// <summary>
        /// Verilen TC Kimlik Numarasını doğrular (Static kullanım).
        /// Varsayılan seçenekleri kullanır.
        /// </summary>
        /// <param name="tcKimlikNo">Doğrulanacak TC Kimlik No.</param>
        /// <returns>Geçerliyse true, değilse false döner.</returns>
        public static bool IsValid(string? tcKimlikNo)
        {
            return new TcKimlikNoValidator().Validate(tcKimlikNo).IsValid;
        }

        /// <summary>
        /// Verilen TC Kimlik Numarasını detaylı olarak doğrular.
        /// </summary>
        /// <param name="tcKimlikNo">Doğrulanacak TC Kimlik No.</param>
        /// <returns>Doğrulama sonucunu içeren ValidationResult nesnesi.</returns>
        public ValidationResult Validate(string? tcKimlikNo)
        {
            var messages = TurkishValidatorConfig.GetMessages(_options.Culture);

            if (string.IsNullOrWhiteSpace(tcKimlikNo))
            {
                return ValidationResult.Failure(messages.TcKimlikNoEmpty, "TcKimlikNoEmpty");
            }

            string valueToValidate = tcKimlikNo;

            // Otomatik normalize (sadece rakamları al)
            if (_options.AutoNormalize)
            {
                valueToValidate = new string(tcKimlikNo.Where(char.IsDigit).ToArray());
            }

            // Uzunluk kontrolü (11 hane)
            if (valueToValidate.Length != 11)
            {
                return ValidationResult.Failure(messages.TcKimlikNoLength, "TcKimlikNoLength");
            }

            // Sayısal kontrol
            if (!long.TryParse(valueToValidate, out _))
            {
                return ValidationResult.Failure(messages.TcKimlikNoNumeric, "TcKimlikNoNumeric");
            }

            // İlk hane 0 olamaz
            if (valueToValidate[0] == '0')
            {
                return ValidationResult.Failure(messages.TcKimlikNoStartsWithZero, "TcKimlikNoStartsWithZero");
            }

            // Algoritma kontrolü (Strict mode)
            if (_options.StrictMode)
            {
                if (!_options.AllowTestNumbers)
                {
                    if (valueToValidate == "11111111110")
                    {
                        return ValidationResult.Failure(messages.TcKimlikNoChecksum, "TcKimlikNoChecksum");
                    }
                }

                if (!ValidateChecksum(valueToValidate))
                {
                    return ValidationResult.Failure(messages.TcKimlikNoChecksum, "TcKimlikNoChecksum");
                }
            }

            return ValidationResult.Success();
        }

        private bool ValidateChecksum(string tcNo)
        {
            int[] digits = tcNo.Select(c => c - '0').ToArray();

            int d1 = digits[0];
            int d2 = digits[1];
            int d3 = digits[2];
            int d4 = digits[3];
            int d5 = digits[4];
            int d6 = digits[5];
            int d7 = digits[6];
            int d8 = digits[7];
            int d9 = digits[8];
            int d10 = digits[9];
            int d11 = digits[10];

            int odds = d1 + d3 + d5 + d7 + d9;
            int evens = d2 + d4 + d6 + d8;

            int calculatedD10 = ((odds * 7) - evens) % 10;
            if (calculatedD10 < 0) calculatedD10 += 10;

            if (calculatedD10 != d10) return false;

            int sumFirst10 = 0;
            for (int i = 0; i < 10; i++) sumFirst10 += digits[i];

            int calculatedD11 = sumFirst10 % 10;

            if (calculatedD11 != d11) return false;

            return true;
        }
    }
}
