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
        private static readonly Dictionary<string, ValidationMessages> _customMessages = new Dictionary<string, ValidationMessages>();

        /// <summary>
        /// Varsayılan mesaj setini ayarlar.
        /// </summary>
        public static void SetDefaultMessages(ValidationMessages messages)
        {
            _defaultMessages = messages;
        }

        /// <summary>
        /// Belirli bir kültür kodu için özel mesaj seti kaydeder.
        /// Örnek: "de-DE", "az-Latn-AZ" veya mevcut "tr-TR"yi ezmek için.
        /// </summary>
        /// <param name="cultureCode">Kültür kodu (örn: "en-US")</param>
        /// <param name="messages">Mesaj seti</param>
        public static void RegisterMessages(string cultureCode, ValidationMessages messages)
        {
            if (string.IsNullOrWhiteSpace(cultureCode)) throw new ArgumentNullException(nameof(cultureCode));
            if (messages == null) throw new ArgumentNullException(nameof(messages));

            _customMessages[cultureCode] = messages;
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

            // 1. Önce kayıtlı özel mesajlara bak (Tam eşleşme)
            if (_customMessages.TryGetValue(targetCulture.Name, out var customMsg))
            {
                return customMsg;
            }

            // 2. Dil koduna göre bak (örn: "en-US" yoksa "en" var mı?)
            if (_customMessages.TryGetValue(targetCulture.TwoLetterISOLanguageName, out var customMsgLang))
            {
                return customMsgLang;
            }

            if (_defaultMessages != null && culture == null) 
            {
                return _defaultMessages;
            }

            // 3. Basit dil kontrolü (Hardcoded varsayılanlar)
            if (targetCulture.TwoLetterISOLanguageName == "en")
            {
                return ValidationMessages.CreateEnglish();
            }

            return _defaultMessages ?? ValidationMessages.CreateDefault();
        }
    }
}
