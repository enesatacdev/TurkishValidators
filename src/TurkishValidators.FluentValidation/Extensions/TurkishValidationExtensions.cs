using FluentValidation;
using TurkishValidators.Validators;
using TurkishValidators.Options;

namespace TurkishValidators.FluentValidation.Extensions
{
    public static class TurkishValidationExtensions
    {
        public static IRuleBuilderOptions<T, string> MustBeTurkishIdentity<T>(
            this IRuleBuilder<T, string> ruleBuilder,
            TcKimlikNoValidationOptions? options = null)
        {
            return ruleBuilder.Must(value =>
            {
                var validator = new TcKimlikNoValidator(options);
                return validator.Validate(value).IsValid;
            });
        }

        public static IRuleBuilderOptions<T, string> MustBeTurkishIban<T>(
            this IRuleBuilder<T, string> ruleBuilder,
            IbanValidationOptions? options = null)
        {
            return ruleBuilder.Must(value =>
            {
                var validator = new IbanValidator(options);
                return validator.Validate(value).IsValid;
            });
        }

        public static IRuleBuilderOptions<T, string> MustBeTurkishPhone<T>(
            this IRuleBuilder<T, string> ruleBuilder,
            TelefonNoValidationOptions? options = null)
        {
            return ruleBuilder.Must(value =>
            {
                var validator = new TelefonNoValidator(options);
                return validator.Validate(value).IsValid;
            });
        }

        public static IRuleBuilderOptions<T, string> MustBeVehiclePlate<T>(
            this IRuleBuilder<T, string> ruleBuilder,
            PlakaValidationOptions? options = null)
        {
            return ruleBuilder.Must(value =>
            {
                var validator = new PlakaValidator(options);
                return validator.Validate(value).IsValid;
            });
        }

        public static IRuleBuilderOptions<T, string> MustBeTurkishTaxNumber<T>(
            this IRuleBuilder<T, string> ruleBuilder,
            VergiNoValidationOptions? options = null)
        {
            return ruleBuilder.Must(value =>
            {
                var validator = new VergiNoValidator(options);
                return validator.Validate(value).IsValid;
            });
        }

        public static IRuleBuilderOptions<T, string> MustBePostalCode<T>(
            this IRuleBuilder<T, string> ruleBuilder,
            PostaKoduValidationOptions? options = null)
        {
            return ruleBuilder.Must(value =>
            {
                var validator = new PostaKoduValidator(options);
                return validator.Validate(value).IsValid;
            });
        }
    }
}
