namespace TurkishValidators.Enums
{
    /// <summary>
    /// Maskeleme işleminin nasıl yapılacağını belirler.
    /// </summary>
    public enum MaskingMode
    {
        /// <summary>
        /// Özel ayarlar (VisibleStart/VisibleEnd) kullanılır.
        /// </summary>
        Custom = 0,

        /// <summary>
        /// Tüm karakterleri maskeler (örn: ***********).
        /// </summary>
        All = 1,

        /// <summary>
        /// Sadece ilk 2 karakteri gösterir (örn: 12*********).
        /// </summary>
        ShowFirstTwo = 2,

        /// <summary>
        /// Sadece ilk 3 karakteri gösterir (örn: 123********).
        /// </summary>
        ShowFirstThree = 3,

        /// <summary>
        /// Sadece son 2 karakteri gösterir (örn: *********02).
        /// </summary>
        ShowLastTwo = 4,

        /// <summary>
        /// Sadece son 3 karakteri gösterir (örn: ********802).
        /// </summary>
        ShowLastThree = 5,

        /// <summary>
        /// Sadece son 4 karakteri gösterir (örn: *******7802).
        /// </summary>
        ShowLastFour = 6
    }
}
