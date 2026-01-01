using System.Globalization;

namespace TurkishValidators.Options
{
    /// <summary>
    /// Araç plakası doğrulama seçenekleri.
    /// </summary>
    public class PlakaValidationOptions
    {
        /// <summary>
        /// Resmi (siyah), yeşil, kırmızı plakalar gibi özel plakalara izin verilip verilmediği.
        /// Şimdilik standart sivil plakalar baz alınmıştır.
        /// </summary>
        public bool AllowSpecialPlates { get; set; } = false;

        /// <summary>
        /// Kültür bilgisi.
        /// </summary>
        public CultureInfo? Culture { get; set; }
    }
}
