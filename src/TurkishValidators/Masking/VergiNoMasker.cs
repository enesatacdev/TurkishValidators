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

            if (opts.Mode != Enums.MaskingMode.Custom)
            {
                ApplyModeSettings(opts, vergiNo.Length);
            }
            
            if (vergiNo.Length <= opts.VisibleStart + opts.VisibleEnd) return vergiNo;

            string start = vergiNo.Substring(0, opts.VisibleStart);
            string end = vergiNo.Substring(vergiNo.Length - opts.VisibleEnd);
            string middle = new string(opts.MaskChar, vergiNo.Length - (opts.VisibleStart + opts.VisibleEnd));

            return start + middle + end;
        }

        public static string Mask(string? vergiNo, Enums.MaskingMode mode)
        {
            return Mask(vergiNo, new MaskingOptions { Mode = mode });
        }

        private static void ApplyModeSettings(MaskingOptions opts, int length)
        {
            switch (opts.Mode)
            {
                case Enums.MaskingMode.All:
                    opts.VisibleStart = 0;
                    opts.VisibleEnd = 0;
                    break;
                case Enums.MaskingMode.ShowFirstTwo:
                    opts.VisibleStart = 2;
                    opts.VisibleEnd = 0;
                    break;
                case Enums.MaskingMode.ShowFirstThree:
                    opts.VisibleStart = 3;
                    opts.VisibleEnd = 0;
                    break;
                case Enums.MaskingMode.ShowLastTwo:
                    opts.VisibleStart = 0;
                    opts.VisibleEnd = 2;
                    break;
                case Enums.MaskingMode.ShowLastThree:
                    opts.VisibleStart = 0;
                    opts.VisibleEnd = 3;
                    break;
                case Enums.MaskingMode.ShowLastFour:
                    opts.VisibleStart = 0;
                    opts.VisibleEnd = 4;
                    break;
            }
        }

        public static string MaskFully(string? vergiNo)
        {
            if (string.IsNullOrEmpty(vergiNo)) return string.Empty;
            return new string('*', vergiNo.Length);
        }
    }
}
