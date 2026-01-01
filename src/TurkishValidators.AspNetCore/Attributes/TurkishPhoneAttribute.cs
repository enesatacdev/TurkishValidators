using System.ComponentModel.DataAnnotations;
using TurkishValidators.Validators;

namespace TurkishValidators.AspNetCore.Attributes
{
    /// <summary>
    /// Türk Telefon Numarası doğrulama niteliği.
    /// </summary>
    public class TurkishPhoneAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            string? stringValue = value.ToString();
            var result = new TelefonNoValidator().Validate(stringValue);

            if (result.IsValid) return ValidationResult.Success;

            return new ValidationResult(ErrorMessage ?? result.ErrorMessage);
        }
    }
}
