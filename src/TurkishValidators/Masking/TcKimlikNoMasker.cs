using System;
using TurkishValidators.Options;

namespace TurkishValidators.Masking
{
    /// <summary>
    /// TC Kimlik Numarası maskeleme işlemlerini sağlar.
    /// </summary>
    public static class TcKimlikNoMasker
    {
        /// <summary>
        /// TC Kimlik Numarasını maskeler.
        /// Varsayılan: İlk 3 ve son 2 hane görünür, kalanı maskelenir (123******01).
        /// </summary>
        /// <param name="tcNo">Maskelenecek TC No.</param>
        /// <param name="options">Maskeleme seçenekleri.</param>
        /// <returns>Maskelenmiş string.</returns>
        public static string Mask(string? tcNo, MaskingOptions? options = null)
        {
            if (string.IsNullOrEmpty(tcNo)) return string.Empty;

            var opts = options ?? new MaskingOptions { VisibleStart = 3, VisibleEnd = 2 };
            
            if (tcNo.Length <= opts.VisibleStart + opts.VisibleEnd)
            {
                return tcNo; // Maskelenecek kadar uzun değilse olduğu gibi dön
            }

            string start = tcNo.Substring(0, opts.VisibleStart);
            string end = tcNo.Substring(tcNo.Length - opts.VisibleEnd);
            string middle = new string(opts.MaskChar, tcNo.Length - (opts.VisibleStart + opts.VisibleEnd));

            return start + middle + end;
        }

        /// <summary>
        /// TC Kimlik Numarasını tamamen maskeler.
        /// </summary>
        public static string MaskFully(string? tcNo)
        {
            if (string.IsNullOrEmpty(tcNo)) return string.Empty;
            return new string('*', tcNo.Length);
        }
    }
}
