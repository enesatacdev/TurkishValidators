using System.Collections.Generic;
using System.Linq;
using TurkishValidators.Options;
using TurkishValidators.Results;
using TurkishValidators.Config;
using TurkishValidators.Resources;

namespace TurkishValidators.Validators
{
    /// <summary>
    /// Posta kodu doğrulama işlemlerini gerçekleştirir.
    /// </summary>
    public class PostaKoduValidator
    {
        private readonly PostaKoduValidationOptions _options;

        public PostaKoduValidator(PostaKoduValidationOptions? options = null)
        {
            _options = options ?? new PostaKoduValidationOptions();
        }

        public static bool IsValid(string? postaKodu)
        {
            return new PostaKoduValidator().Validate(postaKodu).IsValid;
        }

        public ValidationResult Validate(string? postaKodu)
        {
            var messages = TurkishValidatorConfig.GetMessages(_options.Culture);

            if (string.IsNullOrWhiteSpace(postaKodu))
            {
                return ValidationResult.Failure(messages.PostalCodeEmpty, "PostalCodeEmpty");
            }

            if (postaKodu.Length != 5)
            {
                return ValidationResult.Failure(messages.PostalCodeLength, "PostalCodeLength");
            }

            if (!int.TryParse(postaKodu, out int code))
            {
                return ValidationResult.Failure(messages.PostalCodeNumeric, "PostalCodeNumeric");
            }

            int cityCode = code / 1000;
            if (cityCode < 1 || cityCode > 81)
            {
                return ValidationResult.Failure(messages.PostalCodeCityInvalid, "PostalCodeCityInvalid");
            }

            if (_options.ValidateCity && !string.IsNullOrEmpty(_options.ExpectedCity))
            {
                int? expectedCityCode = CityData.GetPlateCode(_options.ExpectedCity);
                if (expectedCityCode.HasValue && expectedCityCode.Value != cityCode)
                {
                    return ValidationResult.Failure(messages.PostalCodeCityInvalid, "PostalCodeCityMismatch");
                }
            }

            var result = ValidationResult.Success();
            result.Metadata["CityCode"] = cityCode;
            if (CityData.Cities.ContainsKey(cityCode))
            {
                result.Metadata["CityName"] = CityData.Cities[cityCode];
            }
            return result;
        }
    }
}
