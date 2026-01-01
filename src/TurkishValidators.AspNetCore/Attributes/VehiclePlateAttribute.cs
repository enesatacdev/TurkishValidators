using System.ComponentModel.DataAnnotations;
using TurkishValidators.Validators;

namespace TurkishValidators.AspNetCore.Attributes
{
    /// <summary>
    /// Araç Plakası doğrulama niteliği.
    /// </summary>
    public class VehiclePlateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            string? stringValue = value.ToString();
            var result = new PlakaValidator().Validate(stringValue);

            if (result.IsValid) return ValidationResult.Success;

            return new ValidationResult(ErrorMessage ?? result.ErrorMessage);
        }
    }
}
