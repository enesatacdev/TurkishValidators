using System.Globalization;

namespace TurkishValidators.Options
{
    /// <summary>
    /// IBAN doğrulama seçenekleri.
    /// </summary>
    public class IbanValidationOptions
    {
        public CultureInfo? Culture { get; set; }
    }
}
