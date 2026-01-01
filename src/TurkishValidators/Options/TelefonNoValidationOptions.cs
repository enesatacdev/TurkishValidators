using System.Globalization;

namespace TurkishValidators.Options
{
    /// <summary>
    /// Telefon numarası doğrulama seçenekleri.
    /// </summary>
    public class TelefonNoValidationOptions
    {
        /// <summary>
        /// Sabit hatlara izin verir. Varsayılan: true.
        /// </summary>
        public bool AllowLandlines { get; set; } = true;

        /// <summary>
        /// GSM numaralarına izin verir. Varsayılan: true.
        /// </summary>
        public bool AllowGsm { get; set; } = true;

        /// <summary>
        /// Ülke kodu (+90 veya 90) kullanımını zorunlu tutmaz, varsa kabul eder.
        /// </summary>
        public bool AllowCountryCode { get; set; } = true;

        /// <summary>
        /// Kültür bilgisi.
        /// </summary>
        public CultureInfo? Culture { get; set; }
    }
}
