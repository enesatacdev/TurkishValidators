using System;
using TurkishValidators.Options;

namespace TurkishValidators.Masking
{
    /// <summary>
    /// Vergi Numarası maskeleme işlemlerini sağlar.
    /// </summary>
    public static class VergiNoMasker
    {
        public static string Mask(string? vergiNo, MaskingOptions? options = null)
        {
            if (string.IsNullOrEmpty(vergiNo)) return string.Empty;

            // Varsayılan: İlk 3 ve son 2 hane görünür
            var opts = options ?? new MaskingOptions { VisibleStart = 3, VisibleEnd = 2 };
            
            if (vergiNo.Length <= opts.VisibleStart + opts.VisibleEnd) return vergiNo;

            string start = vergiNo.Substring(0, opts.VisibleStart);
            string end = vergiNo.Substring(vergiNo.Length - opts.VisibleEnd);
            string middle = new string(opts.MaskChar, vergiNo.Length - (opts.VisibleStart + opts.VisibleEnd));

            return start + middle + end;
        }

        public static string MaskFully(string? vergiNo)
        {
            if (string.IsNullOrEmpty(vergiNo)) return string.Empty;
            return new string('*', vergiNo.Length);
        }
    }
}
