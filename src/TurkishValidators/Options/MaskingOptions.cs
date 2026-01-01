namespace TurkishValidators.Options
{
    /// <summary>
    /// Maskeleme seçenekleri.
    /// </summary>
    public class MaskingOptions
    {
        /// <summary>
        /// Maskeleme için kullanılacak karakter. Varsayılan: '*'.
        /// </summary>
        public char MaskChar { get; set; } = '*';

        /// <summary>
        /// Başlangıçta kaç karakterin görünür olacağı.
        /// </summary>
        public int VisibleStart { get; set; } = 0;

        /// <summary>
        /// Sonda kaç karakterin görünür olacağı.
        /// </summary>
        public int VisibleEnd { get; set; } = 0;

        /// <summary>
        /// Maskeleme modu. Varsayılan: Custom (VisibleStart/End kullanır).
        /// </summary>
        public TurkishValidators.Enums.MaskingMode Mode { get; set; } = TurkishValidators.Enums.MaskingMode.Custom;
    }
}
