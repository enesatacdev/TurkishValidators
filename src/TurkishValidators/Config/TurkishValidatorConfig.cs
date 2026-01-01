using System.Globalization;
using TurkishValidators.Resources;

namespace TurkishValidators.Config
{
    /// <summary>
    /// Kütüphane genelinde geçerli konfigürasyon ayarları.
    /// </summary>
    public static class TurkishValidatorConfig
    {
        private static ValidationMessages? _defaultMessages;
        private static CultureInfo _defaultCulture = new CultureInfo("tr-TR");

        /// <summary>
        /// Varsayılan mesaj setini ayarlar.
        /// </summary>
        public static void SetDefaultMessages(ValidationMessages messages)
        {
            _defaultMessages = messages;
        }

        /// <summary>
        /// Varsayılan kültürü ayarlar.
        /// </summary>
        public static void SetDefaultCulture(CultureInfo culture)
        {
            _defaultCulture = culture;
        }

        /// <summary>
        /// Geçerli mesaj setini getirir.
        /// </summary>
        public static ValidationMessages GetMessages(CultureInfo? culture = null)
        {
            CultureInfo targetCulture = culture ?? _defaultCulture;

            if (_defaultMessages != null && culture == null) 
            {
                return _defaultMessages;
            }

            // Basit dil kontrolü
            if (targetCulture.TwoLetterISOLanguageName == "en")
            {
                return ValidationMessages.CreateEnglish();
            }

            return _defaultMessages ?? ValidationMessages.CreateDefault();
        }
    }
}
