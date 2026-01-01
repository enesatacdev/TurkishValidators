using System.Globalization;

namespace TurkishValidators.Options
{
    /// <summary>
    /// TC Kimlik Numarası doğrulama seçenekleri.
    /// </summary>
    public class TcKimlikNoValidationOptions
    {
        /// <summary>
        /// Doğrulama öncesinde rakam olmayan karakterlerin otomatik olarak temizlenmesini sağlar. Varsayılan: false.
        /// </summary>
        public bool AutoNormalize { get; set; }

        /// <summary>
        /// Katı doğrulama kurallarını (algoritma kontrolü) uygular. Varsayılan: true.
        /// </summary>
        public bool StrictMode { get; set; } = true;

        /// <summary>
        /// Test amaçlı numaralara (örn. 11111111110) izin verilir. Varsayılan: false.
        /// </summary>
        public bool AllowTestNumbers { get; set; } = false;

        /// <summary>
        /// Hata mesajları için kullanılacak kültür bilgisi.
        /// </summary>
        public CultureInfo? Culture { get; set; }
    }
}
