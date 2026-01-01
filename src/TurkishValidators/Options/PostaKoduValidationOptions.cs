using System.Globalization;

namespace TurkishValidators.Options
{
    /// <summary>
    /// Posta kodu doğrulama seçenekleri.
    /// </summary>
    public class PostaKoduValidationOptions
    {
        /// <summary>
        /// Şehir doğrulamasını etkinleştirir. 
        /// Etkinse, posta kodunun geçerli bir il plakası ile başlayıp başlamadığı kontrol edilir.
        /// Varsayılan: false.
        /// </summary>
        public bool ValidateCity { get; set; } = false;

        /// <summary>
        /// Doğrulama sırasında beklenilen şehir ismi (Örn: "İstanbul", "Ankara").
        /// Eşleşmezse hata döner.
        /// </summary>
        public string? ExpectedCity { get; set; }

        public CultureInfo? Culture { get; set; }
    }
}
