using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TurkishValidators.Options;
using TurkishValidators.Results;
using TurkishValidators.Config;

using TurkishValidators.Resources;

namespace TurkishValidators.Validators
{
    /// <summary>
    /// Türkiye araç plakası doğrulama işlemlerini gerçekleştirir.
    /// </summary>
    public class PlakaValidator
    {
        private readonly PlakaValidationOptions _options;
        
        public PlakaValidator(PlakaValidationOptions? options = null)
        {
            _options = options ?? new PlakaValidationOptions();
        }

        public static bool IsValid(string? plaka)
        {
            return new PlakaValidator().Validate(plaka).IsValid;
        }

        public ValidationResult Validate(string? plaka)
        {
            var messages = TurkishValidatorConfig.GetMessages(_options.Culture);

            if (string.IsNullOrWhiteSpace(plaka))
            {
                return ValidationResult.Failure(messages.PlateEmpty, "PlateEmpty");
            }

            string cleanPlate = plaka.Replace(" ", "").ToUpperInvariant();

            var regex = new Regex(@"^(\d{2})([A-Z]{1,3})(\d{2,5})$");
            var match = regex.Match(cleanPlate);

            if (!match.Success)
            {
                 return ValidationResult.Failure(messages.PlateFormat, "PlateFormat");
            }

            int cityCode = int.Parse(match.Groups[1].Value);

            if (cityCode < 1 || cityCode > 81)
            {
                return ValidationResult.Failure(messages.PlateCityCode, "PlateCityCode");
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
